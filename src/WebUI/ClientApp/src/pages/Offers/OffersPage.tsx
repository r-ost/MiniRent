import { ReactNode, useEffect, useState } from "react"
import { CarRentalOffer } from "../../components/CarRentalOffer/CarRentalOffer"
import { loginRequest } from "../../app/authConfig";
import { useMsal } from "@azure/msal-react";
import { IVehiclesService } from "../../app/services/VehiclesService";
import { IVehicleDto } from "../../app/web-api-client";
import { callApiAuthenticated } from "../../app/ApiHelpers";
import { IRentalService } from "../../app/services/RentalService";



export interface CarDetails {
    id: string | undefined,
    renter: string | undefined,
    capacity: number | undefined,
    enginePower: string | undefined,
    yearOfProduction: string | undefined,
    description: string | undefined,
    price: number | undefined,
    currency: string | undefined,
    quoteId: string | undefined
}

interface OffersPageProps {
    vehiclesService: IVehiclesService,
    rentalService: IRentalService
}

export const OffersPage: React.FC<OffersPageProps> = (props) => {

    const { instance, accounts } = useMsal();
    const [carOffers, setCarOffers] = useState(new Map<string, Array<CarDetails>>());
    const [offersExpanded, setOffersExpanded] = useState(new Map<string, boolean>());
    const [startDate, setStartDate] = useState(new Date());

    const handleOffersExpandedChange = (newValue: boolean, key: { brand: string, model: string }) => {
        let items = new Map<string, boolean>(offersExpanded);
        items.set(JSON.stringify(key), newValue);
        setOffersExpanded(items);
    }

    const getPrice = (brand: string, model: string, location: string, renter: string) => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.rentalService.getPrice(accessToken, brand, model, location, 1).then((response) => {
                let offers = new Map<string, Array<CarDetails>>(carOffers);
                let val = offers.get(JSON.stringify({ brand: brand, model: model }));
                var details = val?.find(x => x.renter == renter);
                if (details != null) {
                    details.price = response.price;
                    details.currency = response.currency;
                    details.quoteId = response.quotaId;
                }

                setCarOffers(offers);
            })
        })
    }

    const rentCar = (quoteId: string, brand: string, model: string, renter: string, carId: string) => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.rentalService.rentCar(accessToken, quoteId, startDate, carId).then((response) => {
                let offers = new Map<string, Array<CarDetails>>(carOffers);
                let val = offers.get(JSON.stringify({ brand: brand, model: model }));
                var details = val?.find(x => x.renter == renter);
                if (details != null) {
                    details.price = undefined;
                    details.currency = undefined;
                    details.quoteId = undefined;
                }

                setCarOffers(offers);
            });
        })
    }


    const fetchCarOffers = () => {
        const processResponse = (response: IVehicleDto[]) => {
            let map = new Map<string, Array<CarDetails>>();
            response.forEach(c => {
                let key = JSON.stringify({ brand: c.brandName, model: c.modelName });
                let vehicle = {
                    renter: c.rentCompany, capacity: c.capacity, description: c.description,
                    enginePower: c.enginePower?.toString() + " HP", yearOfProduction: c.year?.toString(),
                    price: undefined, currency: undefined, quoteId: undefined, id: c.id
                };

                if (map.has(key)) {
                    map.get(key)?.push(vehicle);
                } else {
                    let arr = new Array<CarDetails>();
                    arr.push(vehicle);
                    map.set(key, arr);
                }
            });
            setCarOffers(map);
            let offersExpanded = new Map<string, boolean>();
            for (let k in map.keys) {
                if (offersExpanded.has(k) === false) {
                    offersExpanded.set(k, false);
                }
            }
            setOffersExpanded(offersExpanded);
        }

        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.vehiclesService.getVehicles(accessToken).then(response => {
                processResponse(response);
            });
        })
    }

    useEffect(() => {
        fetchCarOffers();
    }, []);

    let elements = new Array<ReactNode>();
    let i = 0;
    carOffers.forEach((value, key) => {
        let k: { brand: string, model: string } = JSON.parse(key);
        elements.push(<CarRentalOffer key={i} model={k.model} brand={k.brand} expanded={offersExpanded.get(key)}
            expandedChanged={(newValue) => handleOffersExpandedChange(newValue, k)} carsDetails={value}
            getPrice={getPrice}
            rentCar={rentCar}></CarRentalOffer >);
        i++;
    })

    return (
        <div className="space-y-5 px-52 py-10">
            {elements.map(x => x)}
        </div >
    )
}