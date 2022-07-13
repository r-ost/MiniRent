import { Button } from "antd";
import { CurrentRentalDto } from "../../app/web-api-client";
import { HistoricRentalDto } from "../../app/web-api-client";

export const HistoricRentalDetailsItem = (props: {
    details: HistoricRentalDto,
}) => {


    return (
        <div className="border border-black my-6 w-full p-3 flex-col">
            <div className="flex justify-items-stretch">
                <div className="flex-1">
                    <div className="font-bold text-lg">{props.details.carDetailsDto?.brand}</div>
                    <div className="text-lg">{props.details.carDetailsDto?.model}</div>
                    <div className="mt-2 italic">{props.details.rentCompanyName}</div>
                </div>
                <div className="flex-1">
                    <div>Capacity: {props.details.carDetailsDto?.capacity}</div>
                    <div>Engine power: {props.details.carDetailsDto?.enginePower}</div>
                    <div>Year: {props.details.carDetailsDto?.yearOfProduction}</div>
                </div>
                <div className="flex-1">
                    Description:
                    <div>{props.details.carDetailsDto?.description}</div>
                </div>
                <div className="flex-col">
                    <div className="border-2 px-4 py-2 text-base flex">
                        <div className="mr-6 ">
                            <div>From:</div>
                            <div>To:</div>
                        </div>
                        <div>
                            <div>{props.details.dateFrom?.toISOString().substring(0, 10)}</div>
                            <div>{props.details.dateTo?.toISOString().substring(0, 10)}</div>
                        </div>
                    </div>
                    <div className="mt-3 ">Total Price: {props.details.totalPrice} {props.details.currency}</div>
                </div>
            </div>




        </div>
    )
}