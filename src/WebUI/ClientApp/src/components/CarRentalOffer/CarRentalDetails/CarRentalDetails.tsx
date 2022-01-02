import { CarDetails } from "../../../pages/Offers/OffersPage"
import Loader from "react-loader-spinner"


interface CarRentalDetailsProps {
    details: CarDetails,
    mode: string,
    price: number
}

export const CarRentalDetails: React.FC<CarRentalDetailsProps> = (props) => {


    const renderButton = () => {
        switch (props.mode) {
            case "checkPrice":
                return <button className="bg-orange-400 hover:bg-orange-300 rounded-md w-full h-full p-2">Check price</button>;
            case "rent":
                return <div className="flex-col  w-full ">
                    <div className="font-bold ml-auto flex-none w-fit text-lg">{props.price} PLN/day</div>
                    <button className="bg-blue-500 hover:bg-blue-400 rounded-md w-full h-full p-2 text-white">Rent</button>
                </div>;
            case "fetching":
                return <div className="ml-auto">
                    <Loader type="Circles" color="#FF8C00" height={50} width={50} />
                </div>
        }
    }

    return (
        <div className="border-black border ">
            <div className="flex space-x-5 items-center p-6">
                <div className="w-2/5 flex items-center">
                    <div className="rounded-full bg-black w-3 h-3 mr-4"></div>
                    {props.details.renter}
                </div>
                <div className="w-1/5">
                    <div>Capacity: {props.details.capacity}</div>
                    <div>Engine power: {props.details.enginePower}</div>
                    <div>Production year: {props.details.yearOfProduction}</div>
                </div>
                <div className="w-1/5">
                    <div className="mb-3">Description:</div>
                    {props.details.description}
                </div>
                <div className="w-1/5 h-14 ml-auto flex">
                    {renderButton()}
                </div>
            </div>
        </div>
    )
}