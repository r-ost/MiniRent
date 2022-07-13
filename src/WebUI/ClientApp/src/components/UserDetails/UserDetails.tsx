import { useEffect, useState } from "react";
import { UserDetailsDto } from "../../app/web-api-client";
import dateFormat from 'dateformat'




interface UserDetailsProps{
    userDetails: UserDetailsDto;
}

export const UserDetails: React.FC<UserDetailsProps> = (props) => {

    return (
        <div className="overflow-y-auto max-h-full p-6">
            <div className="grid grid-cols-5 gap-3">
                <div className="some-class">
                    <div>Name:</div>
                    <div>Surname:</div>
                    <div>Login: </div>
                    <div>Driver License since:</div>
                    <div>Date of birth:</div>
                    <div>Email address:</div>
                </div>
                <div className="col-span-4">
                    <div> {props.userDetails.name ?? ""}</div>
                    <div> {props.userDetails.surname ?? ""}</div>
                    <div> {props.userDetails.login ?? ""}</div>
                    <div> {props.userDetails.licenceObtainmentYear ?? ""}</div>
                    <div> {props.userDetails.dateOfBirth != null ? dateFormat(props.userDetails.dateOfBirth, "yyyy-mm-dd") : ""}</div>
                    <div> {props.userDetails.emailAddress ?? ""}</div>
                </div> 
            </div>
        </div>
    )
}