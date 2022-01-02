import { FormEvent, useState } from "react";
import "./RegisterPage.css"
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useForm } from "react-hook-form";

interface Address {
    country: string,
    street: string,
    city: string,
    zipCode: string
}

interface FormData {
    firstName: string,
    surname: string,
    email: string,
    address: Address,
    dateOfBirth: Date
    licenseSinceYear: string
}

export const RegisterPage: React.FC = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<FormData>();
    const [dateOfBirth, setDateOfBirth] = useState<Date>(new Date());

    const dateOfBirthChanged = (date: Date | [Date | null, Date | null] | null) => {
        if (date instanceof Date) {
            setDateOfBirth(date);
        }
    }

    const onFormSubmit = handleSubmit((data) => {
        // TODO
    });


    return (
        <div className="wrapper">
            <div className="register-form">
                <div className="create-text">Create new account</div>
                <form onSubmit={onFormSubmit}>
                    <div className="row">
                        <div className="column field">
                            First name*:
                        </div>
                        <div className="column field-input">
                            <input type="text"
                                {...register("firstName", { required: true })}
                                className="form-control" />
                            <div className="text-red-500">{errors.firstName?.type === 'required' && "First name is required"}</div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="column field">
                            Surname*:
                        </div>
                        <div className="column field-input">
                            <input type="text"
                                {...register("surname", { required: true })}
                                className="form-control" />
                            <div className="text-red-500">{errors.surname?.type === 'required' && "Surname is required"}</div>
                        </div>
                    </div>


                    <div className="row">
                        <div className="column field">
                            Email*:
                        </div>
                        <div className="column field-input">
                            <input type="text"
                                {...register("email", { required: true, pattern: /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/ })}
                                className="form-control" />
                            <div className="text-red-500">{errors.email?.type === 'required' && "Email is required"}</div>
                            <div className="text-red-500">{errors.email?.type === 'pattern' && "Email is not in the correct format"}</div>
                        </div>
                    </div>

                    <div className="row">
                        <div>Address* :</div>
                        <div className="address-wrapper">
                            <div className="row">
                                <div className="column field">
                                    Country*:
                                </div>
                                <div className="column field-input">
                                    <input type="text"
                                        {...register("address.country", { required: true })}
                                        className="form-control" />
                                    <div className="text-red-500">{errors.address?.country?.type === 'required' && "Country is required"}</div>
                                </div>
                            </div>

                            <div className="row">
                                <div className="column field">
                                    Street*:
                                </div>
                                <div className="column field-input">
                                    <input type="text"
                                        {...register("address.street", { required: true })}
                                        className="form-control" />
                                    <div className="text-red-500">{errors.address?.street?.type === 'required' && "Street is required"}</div>
                                </div>
                            </div>

                            <div className="city row">
                                <div className="column field">City*:</div>
                                <div className="column field-input">
                                    <input type="text"
                                        {...register("address.city", { required: true })}
                                        className="form-control " />
                                    <div className="text-red-500">{errors.address?.city?.type === 'required' && "City is required"}</div>
                                </div>
                            </div>

                            <div className="zip-code row mb-0">
                                <div className="column field">Zip code*:</div>
                                <div className="column field-input">
                                    <input type="text"
                                        {...register("address.zipCode", { required: true, pattern: /[0-9]{2}-[0-9]{3}/ })}
                                        className="form-control" />
                                    <div className="text-red-500">{errors.address?.zipCode?.type === 'required' && "Zip code is required"}</div>
                                    <div className="text-red-500">{errors.address?.zipCode?.type === 'pattern' && "Zip code is not in the correct format"}</div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="column field">
                            Date of birth*:
                        </div>
                        <div className="column field-input">
                            <DatePicker selected={dateOfBirth}
                                onChange={dateOfBirthChanged}
                                dateFormat="dd/MM/yyyy"
                                className="form-control date-of-birth"
                            ></DatePicker>
                            <div className="text-red-500">{dateOfBirth === null && "Date of birth is required"}</div>
                        </div>
                    </div>


                    <div className="row">
                        <div className="column field">
                            Driver license obtainment year*:
                        </div>
                        <div className="column field-input">
                            <input type="text"
                                {...register("licenseSinceYear", { required: true })}
                                className="form-control license-since" />
                            <div className="text-red-500">{errors.licenseSinceYear?.type === 'required' && "Driver license obtainment year is required"}</div>
                        </div>
                    </div>

                    <div className="register-button">
                        <button type="submit" className="btn btn-primary">Register</button>
                    </div>
                </form>
            </div >
        </div >
    )
}