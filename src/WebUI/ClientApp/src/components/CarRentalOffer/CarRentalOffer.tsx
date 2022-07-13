import { useMsal } from "@azure/msal-react"
import { DatePicker, Input, Modal } from "antd"
import { useState } from "react"
import { callApiAuthenticated } from "../../app/ApiHelpers"
import { IRentalService } from "../../app/services/RentalService"
import { CarDetailsDto } from "../../app/web-api-client"
import { CarDetails } from "../../pages/Offers/OffersPage"
import { CurrentRentals } from "../CurrentRentals/CurrentRentals"
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


interface CurrentRentalDetails {
    brand?: string,
    model?: string,
    renter?: string,
    carId?: string,
    street?: string,
    city?: string,
    zipCode?: string,
    startDate?: Date,
    endDate?: Date
}


const getOneDayBefore = (date: Date) => {
    return new Date(date.getTime() - (1000 * 60 * 60 * 24));
}

const getOneDayAfter = (date: Date) => {
    return new Date(date.getTime() + (1000 * 60 * 60 * 24));
}


export const CarRentalOffer: React.FC<CarRentalOfferProps> = (props) => {

    const [isModalVisible, setIsModalVisible] = useState(false);
    const [currentCarDetails, setCurrentCarDetails] = useState<CarDetails>();
    const [currentRentalDetails, setCurrentRentalDetails] = useState<CurrentRentalDetails>();
    const [modalError, setModalError] = useState("");

    const showModal = (carDetails: CarDetails) => {
        setIsModalVisible(true);
        setCurrentCarDetails(carDetails);
    };

    const handleOk = () => {
        if (currentCarDetails && currentRentalDetails && currentRentalDetails.startDate && currentRentalDetails.endDate
            && currentRentalDetails.street && currentRentalDetails.city && currentRentalDetails.zipCode) {
            if (currentCarDetails.quoteId) {
                props.rentCar(props.brand, props.model, currentCarDetails.renter ?? "", currentCarDetails.id ?? "",
                    `${currentRentalDetails?.street};${currentRentalDetails?.city};${currentRentalDetails?.zipCode}`,
                    currentRentalDetails?.startDate ?? new Date(), currentRentalDetails?.endDate ?? new Date());
            }
            setIsModalVisible(false);
        }
        else {
            setModalError("Not every field is completed.");
        }
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    let dateDiff: number | undefined;
    if (currentRentalDetails?.endDate && currentRentalDetails?.startDate) {
        dateDiff = Math.floor((Date.parse(currentRentalDetails?.endDate.toISOString())
            - Date.parse(currentRentalDetails?.startDate.toISOString())) / 86400000);
    }
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
                                    rentCar={() => { showModal(c) }} currency={c.currency}></CarRentalDetails>)}
                        </div>
                    </div>
                }
            </div>

            <Modal title="Finalize car rental" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel}
                width={1000}>
                <div className="flex flex-col text-lg">
                    <div className="flex space-x-2">
                        <div className="flex-1 border-2 p-6">
                            <div className="flex">
                                <div>
                                    <div className="mb-4 h-8">From:</div>
                                    <div className="h-8">To:</div>
                                </div>
                                <div className="ml-10 flex flex-col">
                                    <DatePicker size="large" className="mb-4 h-8"
                                        onChange={(date, dateString) => {
                                            let r = { ...currentRentalDetails };
                                            if (dateString != "") {
                                                r.startDate = new Date(dateString);
                                            }
                                            else {
                                                r.startDate = undefined;
                                            }
                                            setCurrentRentalDetails(r);
                                        }} disabledDate={d => !d || (currentRentalDetails?.endDate != undefined && d.isSameOrAfter(currentRentalDetails?.endDate))
                                            || d.isBefore(getOneDayBefore(new Date()))} ></DatePicker>
                                    <DatePicker size="large" className="h-8 " onChange={(date, dateString) => {
                                        let r = { ...currentRentalDetails };
                                        if (dateString != "") {
                                            r.endDate = new Date(dateString);
                                        }
                                        else {
                                            r.endDate = undefined;
                                        }
                                        setCurrentRentalDetails(r);
                                    }} disabledDate={d => !d || (currentRentalDetails?.startDate != undefined && d.isBefore(getOneDayAfter(currentRentalDetails.startDate)))
                                        || d.isBefore(getOneDayBefore(new Date()))} ></DatePicker>
                                </div>
                            </div>
                        </div>
                        <div className="flex-1 border-2 p-6">
                            <div className="flex">
                                <div>
                                    <div className="mb-4 h-8">Price per day:</div>
                                    <div className="h-8">Total price:</div>
                                </div>
                                <div className="ml-10">
                                    <div className="mb-4 h-8">{currentCarDetails?.price} {currentCarDetails?.currency}</div>
                                    <div className="h-8 font-bold">
                                        {currentCarDetails?.price && dateDiff ? dateDiff * currentCarDetails?.price : "<select date>"} {dateDiff && currentCarDetails?.currency}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="border-2 p-8 mt-10">
                        <div className="flex">
                            <div className="mr-32 space-y-5">
                                <div>Street: </div>
                                <div>City:</div>
                                <div>ZipCode:</div>
                            </div>
                            <div className="space-y-5">
                                <Input placeholder="e.g. Prosta 12" width={100} onChange={(e) => {
                                    setCurrentRentalDetails({ ...currentRentalDetails, street: e.target.value })
                                }}></Input>
                                <Input placeholder="e.g. Warszawa" width={100} onChange={(e) => {
                                    setCurrentRentalDetails({ ...currentRentalDetails, city: e.target.value })
                                }}></Input>
                                <Input placeholder="e.g. 00-631" width={100} onChange={(e) => {
                                    setCurrentRentalDetails({ ...currentRentalDetails, zipCode: e.target.value })
                                }}></Input>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="text-red-500">{modalError}</div>
            </Modal >
        </div >
    )
}