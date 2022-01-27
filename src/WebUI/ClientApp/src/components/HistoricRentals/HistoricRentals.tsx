import { CurrentRentalDto, HistoricRentalDto } from "../../app/web-api-client"
import { RentalDetailsItem } from "../CurrentRentals/RentalDetailsItem"
import { HistoricRentalDetailsItem } from "./HistoricRentalDetailsItem"


export interface RentalDetails {
    renterCompany: string;
    dateTo: Date;
    dateFrom: Date;
    carDetails: {
        brand: string;
        model: string;
        capacity: string;
        enginePower: number;
        yearOfProduction: number;
        description: string;
    },
    rentId: string
}

interface HistoricRentalsProps {
    rentals: Array<HistoricRentalDto>;
}

export const HistoricRentals: React.FC<HistoricRentalsProps> = (props) => {

    return (
        <div className="overflow-y-auto max-h-full p-6">
            {props.rentals.map(r => <HistoricRentalDetailsItem details={r}></HistoricRentalDetailsItem>)}
        </div>
    )
}