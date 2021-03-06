import { useMsal } from "@azure/msal-react";
import { Tabs } from "antd";
import { ReactElement, useEffect, useState } from "react"
import { callApiAuthenticated } from "../../app/ApiHelpers";
import { IRentalService } from "../../app/services/RentalService";
import { IUserService } from "../../app/services/UserService";
import { CurrentRentalDto, HistoricRentalDto, UserDetailsDto } from "../../app/web-api-client";
import { CurrentRentals, RentalDetails } from "../../components/CurrentRentals/CurrentRentals";
import { HistoricRentals } from "../../components/HistoricRentals/HistoricRentals";
import { UserDetails } from "../../components/UserDetails/UserDetails";
import { CarDetails } from "../Offers/OffersPage";


const { TabPane } = Tabs;

export const AccountPage = (props: { rentalService: IRentalService, userService: IUserService }) => {

    const { instance } = useMsal();
    const { accounts } = useMsal();

    const [currentRentals, setCurrentRentals] = useState<Array<CurrentRentalDto>>();
    const [historicRentals, setHistoricRentals] = useState<Array<HistoricRentalDto>>();
    const [userDetails, setUserDetails] = useState<UserDetailsDto>();

    useEffect(() => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.rentalService.getCurrentRentals(accessToken).then(response => {
                setCurrentRentals(response);
                console.log(response);
            });

            props.rentalService.getHistoricRentals(accessToken).then(response => {
                setHistoricRentals(response);
            });

            props.userService.getUser(accessToken).then(response => {
                setUserDetails(response);
            });
        });
    }, []);


    const returnCar = (rentID: string, company: string, description: string, odometerValueInKm:number, overallState: string) => {
        callApiAuthenticated(instance, accounts[0], (accessToken) => {
            props.rentalService.returnCar(accessToken, rentID, company, description, odometerValueInKm, overallState).then(response => {
                console.log(response);
            }).then(() => {
                callApiAuthenticated(instance, accounts[0], (accessToken) => {
                    props.rentalService.getCurrentRentals(accessToken).then(response => {
                        setCurrentRentals(response);
                        console.log(response);
                    });
                })
            });
        })
    }



    return (
        <div className="pb-1">
            <h1 className="ml-10 mt-6">My Account</h1>
            <div className="border-2 mx-20 my-10">
                <Tabs tabPosition="left" className="m-2 " type="line">
                    <TabPane tab="Current rentals" key="1" className="h-[40rem]">
                        <CurrentRentals rentals={currentRentals ?? new Array<CurrentRentalDto>()}
                            returnCallback={returnCar} returnButton={false}></CurrentRentals>
                    </TabPane>
                    <TabPane tab="History of rentals" key="2" className="h-[40rem]">
                        <HistoricRentals rentals={historicRentals ?? new Array<HistoricRentalDto>()}></HistoricRentals>
                    </TabPane>
                    <TabPane tab="Account details" key="3">
                        <UserDetails userDetails={userDetails ?? new UserDetailsDto()}></UserDetails>
                    </TabPane>
                </Tabs>
            </div>
        </div>
    )
}