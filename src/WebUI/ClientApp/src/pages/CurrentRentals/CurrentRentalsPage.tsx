import { useMsal } from "@azure/msal-react";
import { Modal } from "antd";
import { useEffect, useState } from "react";
import { callApiAuthenticated } from "../../app/ApiHelpers"
import { IWorkerService } from "../../app/services/WorkerService";
import { CurrentRentalDto } from "../../app/web-api-client";
import { CurrentRentals } from "../../components/CurrentRentals/CurrentRentals"



export const CurrentRentalsPage = (props: {
    workerService: IWorkerService
}) => {

    const { instance } = useMsal();
    const { accounts } = useMsal();
    const [currentRentals, setCurrentRentals] = useState<Array<CurrentRentalDto>>();



    useEffect(() => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.workerService.getCurrentRentals(accessToken).then(response => {
                setCurrentRentals(response);
                console.log(response);
            });
        })
    }, []);


    const returnCar = (rentID: string, description: string,
        odometerValueInKm: number,
        overallState: string) => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.workerService.returnCar(accessToken, rentID, description, odometerValueInKm, overallState).then(response => {
                console.log(response);
            }).then(() => {
                callApiAuthenticated(instance, accounts[0], (accessToken) => {
                    props.workerService.getCurrentRentals(accessToken).then(response => {
                        setCurrentRentals(response);
                        console.log(response);
                    });
                })
            });
        })
    }


    return (
        <div>
            <CurrentRentals rentals={currentRentals ?? new Array<CurrentRentalDto>()}
                returnCallback={returnCar} returnButton={true}></CurrentRentals>


        </div>
    )
} 