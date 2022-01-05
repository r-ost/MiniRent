import { ReactNode, useEffect, useState } from "react"
import { CarRentalOffer } from "../../components/CarRentalOffer/CarRentalOffer"
import { loginRequest } from "../../app/authConfig";
import { useMsal } from "@azure/msal-react";
import { IVehiclesService } from "../../app/services/VehiclesService";
import { IVehicleDto } from "../../app/web-api-client";


interface Car {
    brand: string,
    model: string,
    id: number
}


export interface CarDetails {
    renter: string | undefined,
    capacity: number | undefined,
    enginePower: string | undefined,
    yearOfProduction: string | undefined,
    description: string | undefined,
}

interface OffersPageProps {
    vehiclesService: IVehiclesService
}

export const OffersPage: React.FC<OffersPageProps> = (props) => {

    const { instance, accounts } = useMsal();
    const [carOffers, setCarOffers] = useState(new Map<string, Array<CarDetails>>());
    const [offersExpanded, setOffersExpanded] = useState(new Map<string, boolean>());

    const handleOffersExpandedChange = (newValue: boolean, key: { brand: string, model: string }) => {
        let items = new Map<string, boolean>(offersExpanded);
        items.set(JSON.stringify(key), newValue);
        setOffersExpanded(items);
    }


    const fetchCarOffers = () => {
        const request = {
            ...loginRequest,
            account: accounts[0]
        };

        const processResponse = (response: IVehicleDto[]) => {
            let map = new Map<string, Array<CarDetails>>();
            response.forEach(c => {
                let key = JSON.stringify({ brand: c.brandName, model: c.modelName });
                let vehicle = {
                    renter: c.rentCompany, capacity: c.capacity, description: c.description,
                    enginePower: c.enginePower?.toString() + " HP", yearOfProduction: c.year?.toString()
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
        // Silently acquires an access token which is then attached to a request
        instance.acquireTokenSilent(request).then((response) => {
            props.vehiclesService.getVehicles(response.accessToken).then(response => {
                processResponse(response);
            });
        }).catch((e) => {
            instance.acquireTokenPopup(request).then((response) => {
                props.vehiclesService.getVehicles(response.accessToken).then(response => {
                    processResponse(response);
                });
            });
        });
    }


    // TODO: fetch car offer from api
    useEffect(() => {
        fetchCarOffers();

        // setCarOffers([{
        //     brand: "Volkswagen",
        //     model: "Polo",
        //     details: [
        //         { renter: "Wypożyczalnia BestCars", capacity: 5, enginePower: "130 HP", yearOfProduction: "2017", description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur ducimus numquam dolores fugiat assumenda, explicabo repudiandae mollitia vero dolorem officiis cupiditate tempora, iste incidunt consequuntur enim officia soluta illum at." },
        //         { renter: "Wypożyczalnia Marek-rent", capacity: 5, enginePower: "130 HP", yearOfProduction: "2017", description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur ducimus numquam dolores fugiat assumenda, explicabo repudiandae mollitia vero dolorem officiis cupiditate tempora, iste incidunt consequuntur enim officia soluta illum at." }],
        //     id: 0
        // },
        // {
        //     brand: "Audi",
        //     model: "A4",
        //     details: [
        //         { renter: "Wypożyczalnia BestCars2", capacity: 5, enginePower: "130 HP", yearOfProduction: "2017", description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur ducimus numquam dolores fugiat assumenda, explicabo repudiandae mollitia vero dolorem officiis cupiditate tempora, iste incidunt consequuntur enim officia soluta illum at." },
        //         { renter: "Wypożyczalnia Marek-rent2", capacity: 5, enginePower: "130 HP", yearOfProduction: "2017", description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur ducimus numquam dolores fugiat assumenda, explicabo repudiandae mollitia vero dolorem officiis cupiditate tempora, iste incidunt consequuntur enim officia soluta illum at." }],
        //     id: 1
        // }]);
    }, []);

    let elements = new Array<ReactNode>();
    let i = 0;
    carOffers.forEach((value, key) => {
        let k: { brand: string, model: string } = JSON.parse(key);
        elements.push(<CarRentalOffer key={i} model={k.model} brand={k.brand} expanded={offersExpanded.get(key)}
            expandedChanged={(newValue) => handleOffersExpandedChange(newValue, k)} carsDetails={value} ></CarRentalOffer >);
        i++;
    })

    return (
        <div className="space-y-5 px-52 py-10">
            {elements.map(x => x)}
        </div >
    )
}