import { useMsal } from "@azure/msal-react"
import { callApiAuthenticated } from "../../app/ApiHelpers"
import { IRentalService } from "../../app/services/RentalService"
import { CarDetails } from "../../pages/Offers/OffersPage"
import { CarRentalDetails } from "./CarRentalDetails/CarRentalDetails"

interface CarRentalOfferProps {
    brand: string,
    model: string,
    expanded: boolean | undefined,
    expandedChanged: (newValue: boolean) => void,
    carsDetails: Array<CarDetails>
    getPrice: (brand: string, model: string, location: string, renter: string) => void
    rentCar: (brand: string, model: string, renter: string, carId: string, location: string, startDate: Date, endDate: Date) => void
}

export const CarRentalOffer: React.FC<CarRentalOfferProps> = (props) => {


    // TODO: Add rent dialog
    let location = "lokacja";
    let startDate = new Date(2021, 10, 1);
    let endDate = new Date(2021, 11, 1);

    return (
        <div className="border-2 border-black p-4">
            <div className="flex">
                <div className="text-lg">
                    <div className="font-bold">{props.brand}</div>
                    <div>{props.model}</div>
                </div>

                <div className="ml-auto w-36">
                    <button className={`${props.expanded ? "bg-gray-300 hover:bg-gray-200" : "bg-orange-400 hover:bg-orange-300"}
                         rounded-md w-full h-full p-2`}
                        onClick={() => props.expandedChanged(!props.expanded)}>
                        {props.expanded ? "Collapse" : "Available offers"}
                    </button>
                </div>
            </div >

            <div>
                {
                    props.expanded &&
                    <div>
                        <div className="h-0 border-t border-black my-1"></div>
                        <div className="space-y-3 mt-3">
                            <div className="mb-2">Available at:</div>
                            {props.carsDetails.map(c =>
                                <CarRentalDetails details={c} price={c.price}
                                    getPrice={() => props.getPrice(props.brand, props.model, "test", c.renter ?? "")}
                                    rentCar={() => {
                                        if (c.quoteId) {
                                            console.log("Renting: ");
                                            console.log(JSON.stringify(c));
                                            props.rentCar(props.brand, props.model, c.renter ?? "", c.id ?? "",
                                                location, startDate, endDate);
                                        }
                                    }}></CarRentalDetails>)}
                        </div>
                    </div>
                }
            </div>
        </div>
    )
}