import { useState } from "react";
import DatePicker from "react-datepicker";
import { useNavigate } from "react-router-dom";



export const SearchPanel: React.FC = () => {

    const [pickUpLocation, setPickUpLocation] = useState("");
    const [dateFrom, setDateFrom] = useState<Date | null>(null);
    const [dateTo, setDateTo] = useState<Date | null>(null);
    let navigate = useNavigate();

    const dateFromChanged = (date: Date | [Date | null, Date | null] | null) => {
        if (date instanceof Date) {
            setDateFrom(date);
        }
    }

    const dateToChanged = (date: Date | [Date | null, Date | null] | null) => {
        if (date instanceof Date) {
            setDateTo(date);
        }
    }

    const onFormSubmit = () => {
        // TODO
    }

    return (
        <div className="flex justify-center w-full align-middle mt-40">
            <div className="w-3/5">
                <h1 className="text-center">Car Rental - Search and Compare</h1>
                <div className="border-black border-2 mt-20 h-48 bg-orange-400">
                    <form onSubmit={onFormSubmit} className="flex h-full w-full items-center space-x-3 px-5">
                        <div className="flex-auto w-64">
                            <input placeholder="Pick-up location..." className="form-control border-2 border-black" />
                        </div>
                        <div className=" flex-auto w-16">
                            <DatePicker selected={dateFrom}
                                onChange={dateFromChanged}
                                dateFormat="dd/MM/yyyy"
                                className="form-control border-2 border-black"
                                placeholderText="Date from"
                            ></DatePicker>
                        </div>
                        <div className=" flex-auto w-16">
                            <DatePicker selected={dateTo}
                                onChange={dateToChanged}
                                dateFormat="dd/MM/yyyy"
                                className="form-control border-2 border-black"
                                placeholderText="Date to"
                            ></DatePicker>
                        </div>
                        <button type="submit" className="btn btn-primary w-36" onClick={() => navigate("/offers")}>Search</button>
                    </form>
                </div>
            </div>
        </div>
    )
}