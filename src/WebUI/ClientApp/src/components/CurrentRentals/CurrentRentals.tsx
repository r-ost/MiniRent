import { CurrentRentalDto } from "../../app/web-api-client"
import { RentalDetailsItem } from "./RentalDetailsItem"


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

interface CurrentRentalsProps {
    rentals: Array<CurrentRentalDto>;
    returnCallback: (rentID: string) => void
}

export const CurrentRentals: React.FC<CurrentRentalsProps> = (props) => {




    return (
        <div className="overflow-y-auto max-h-full p-6">
            {props.rentals.map(r => <RentalDetailsItem details={r}
                returnCallback={() => props.returnCallback(r.rentId ?? "")}></RentalDetailsItem>)}
        </div>
    )
}