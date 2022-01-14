import { CurrentRentalDto } from "../../app/web-api-client";
import { RentalDetails } from "./CurrentRentals";

export const RentalDetailsItem = (props: {
    details: CurrentRentalDto,
    returnCallback: () => void
}) => {


    return (
        <div className="border border-black my-6 w-fit p-3">
            <div>Rent company: {props.details.rentCompanyName ?? ""}</div>
            <div>Brand: {props.details.carDetailsDto?.brand ?? ""}</div>
            <div>Model: {props.details.carDetailsDto?.model ?? ""}</div>
            <div>Capacity: {props.details.carDetailsDto?.capacity ?? ""}</div>
            <div>Description: {props.details.carDetailsDto?.description ?? ""}</div>
            <div>Engine power: {props.details.carDetailsDto?.enginePower ?? ""}</div>
            <div>Year: {props.details.carDetailsDto?.yearOfProduction ?? ""}</div>
            <div>Date from: {props.details.dateFrom?.toDateString() ?? ""}</div>
            <div>Date to: {props.details.dateTo?.toDateString() ?? ""}</div>

            <button onClick={() => props.returnCallback()} className="btn btn-primary mt-4">RETURN</button>
        </div>
    )
}