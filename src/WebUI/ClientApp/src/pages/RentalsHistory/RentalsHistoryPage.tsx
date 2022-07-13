import { useMsal } from "@azure/msal-react"
import { useEffect, useState } from "react"
import { callApiAuthenticated } from "../../app/ApiHelpers"
import { IRentalService } from "../../app/services/RentalService"
import { IWorkerService } from "../../app/services/WorkerService"
import { HistoricRentalDto } from "../../app/web-api-client"
import { HistoricRentals } from "../../components/HistoricRentals/HistoricRentals"



export const RentalsHistoryPage = (props: {
    workerService: IWorkerService,
    rentalService: IRentalService
}) => {
    const [historicRentals, setHistoricRentals] = useState<Array<HistoricRentalDto>>();
    const { instance } = useMsal();
    const { accounts } = useMsal();
    
    useEffect(() => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.rentalService.getHistoricRentals(accessToken).then(response => {
                setHistoricRentals(response);
            });
        })
    }, []);

    return (
        <div>
            <HistoricRentals rentals={historicRentals ?? new Array<HistoricRentalDto>()}></HistoricRentals>
        </div>
    )
}