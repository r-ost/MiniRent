import { useMsal } from "@azure/msal-react";
import { ReactElement, useEffect, useState } from "react"
import { callApiAuthenticated } from "../../app/ApiHelpers";
import { IRentalService } from "../../app/services/RentalService";
import { CurrentRentalDto } from "../../app/web-api-client";
import { CurrentRentals, RentalDetails } from "../../components/CurrentRentals/CurrentRentals";
import { CarDetails } from "../Offers/OffersPage";


export const AccountPage = (props: { rentalService: IRentalService }) => {

    const [currentPage, setCurrentPage] = useState(1);
    const { instance } = useMsal();
    const { accounts } = useMsal();

    const [currentRentals, setCurrentRentals] = useState<Array<CurrentRentalDto>>();

    useEffect(() => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.rentalService.getCurrentRentals(accessToken).then(response => {
                setCurrentRentals(response);
                console.log(response);
            });
        })
    }, []);


    const returnCar = (rentID: string) => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.rentalService.returnCar(accessToken, rentID).then(response => {
                console.log(response);
            }).then(() => {
                callApiAuthenticated(instance, accounts[0], (accessToken) => {
                    props.rentalService.getCurrentRentals(accessToken).then(response => {
                        setCurrentRentals(response);
                        console.log(response);
                    });
                })
            });
        })



    }

    let page: ReactElement;
    if (currentPage == 1) {
        page = <CurrentRentals rentals={currentRentals ?? new Array<CurrentRentalDto>()}
            returnCallback={returnCar}></CurrentRentals>
    }
    else {
        page = <div></div>
    }

    return (
        <div className="ml-auto w-2/3">
            <h1>My Account</h1>
            <div>
                <div></div>
                <div>
                    {page}
                </div>
            </div>
        </div>
    )
}