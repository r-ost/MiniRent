import { Input, InputNumber, Modal } from "antd";
import TextArea from "antd/lib/input/TextArea";
import { useState } from "react";
import { CurrentRentalDto } from "../../app/web-api-client"
import { CarDetails } from "../../pages/Offers/OffersPage";
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
    returnCallback: (rentID: string, description: string,
        odometerValueInKm: number,
        overallState: string) => void;
    returnButton: boolean;
}

interface CurrentRentInfo {
    description?: string;
    odometerValueInKm?: number;
    overallState?: string;

}

export const CurrentRentals: React.FC<CurrentRentalsProps> = (props) => {


    const [isModalVisible, setIsModalVisible] = useState(false);
    const [modalError, setModalError] = useState("");

    const [currentRentId, setCurrentRentId] = useState("");
    const [currentRentInfo, setCurrentRentInfo] = useState<CurrentRentInfo>();

    const returnCallback = (rentId: string) => {
        showModal(rentId);
    }

    const showModal = (rentId: string) => {
        setIsModalVisible(true);
        setCurrentRentId(rentId);
    };

    const handleOk = () => {

        if (currentRentId != "" && currentRentInfo?.description != undefined &&
            currentRentInfo?.odometerValueInKm != undefined && currentRentInfo?.overallState != undefined) {
            props.returnCallback(currentRentId, currentRentInfo?.description ?? "",
                currentRentInfo?.odometerValueInKm ?? -1, currentRentInfo?.overallState ?? "");
            setIsModalVisible(false);
        }
        else {
            setModalError("Not every field is completed.");
        }
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };


    return (
        <div className="overflow-y-auto max-h-full p-6">
            {props.rentals.map(r => <RentalDetailsItem details={r}
                returnCallback={() => returnCallback(r.rentId ?? "")} returnButton={props.returnButton}></RentalDetailsItem>)}


            <Modal title="Finalize car rental" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel}
                width={1000}>
                <div className="flex space-x-5">
                    <div>
                        <div>Description:</div>
                        <TextArea placeholder="textarea with clear icon" allowClear onChange={(e) => {
                            let t = { ...currentRentInfo };
                            t.description = e.target.value;
                            setCurrentRentInfo(t);
                        }} />
                    </div>
                    <div>
                        <div>Overall state:</div>
                        <Input onChange={(e) => {
                            let t = { ...currentRentInfo };
                            t.overallState = e.target.value;
                            setCurrentRentInfo(t);
                        }}></Input>
                        <div>Odometer value:</div>
                        <InputNumber<number> onChange={(e) => {
                            let t = { ...currentRentInfo };
                            t.odometerValueInKm = e;
                            setCurrentRentInfo(t);
                        }}></InputNumber>
                    </div>
                    <div>
                        <div>Documents:</div>
                        <div className="border-2">
                            <div>Photo:</div>
                            <Input></Input>
                            <div>Protocol</div>
                            <Input></Input>
                        </div>
                    </div>
                </div>
                <div className="text-red-500">{modalError}</div>
            </Modal >
        </div>
    )
}