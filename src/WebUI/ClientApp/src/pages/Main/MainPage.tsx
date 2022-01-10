import { IPublicClientApplication } from "@azure/msal-browser";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react"
import { LoginScreen } from "../../components/LoginScreen/LoginScreen"
import { loginRequest } from "../../app/authConfig";
import { SearchPanel } from "../../components/SearchPanel/SearchPanel";
import { ReactElement, useEffect, useState } from "react";
import { RegisterPage } from "../Register/RegisterPage";
import { IUserService, UserService } from "../../app/services/UserService";
import { callApiAuthenticated } from "../../app/ApiHelpers";

export const MainPage: React.FC<{ userService: IUserService }> = (props: { userService: IUserService }) => {

    const { instance } = useMsal();
    const { accounts } = useMsal();

    const [userExists, setUserExists] = useState<boolean | undefined>(undefined);
    const [userAuthenticated, setUserAuthenticated] = useState<boolean>(false);


    async function handleLogin(instance: IPublicClientApplication) {
        await instance.loginPopup(loginRequest)
            .then(() => {
                setUserAuthenticated(true);
            })
            .catch(e => {
                console.error(e);
            });
    }

    useEffect(() => {
        if (userAuthenticated == true) {
            callApiAuthenticated(instance, accounts[0], (accessToken) => {
                props.userService.userExists(accessToken).then(response => {
                    setUserExists(response);
                });
            })
        }
    }, [userAuthenticated]);


    let element: ReactElement;
    if (userExists === true) {
        element = <SearchPanel></SearchPanel>
    }
    else if (userExists === false) {
        element = <RegisterPage userService={new UserService()}></RegisterPage>
    }
    else {
        element = <div></div>
    }

    return (
        <div>
            <AuthenticatedTemplate>
                {element}
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                <LoginScreen handleLogin={async () => await handleLogin(instance)}></LoginScreen>
            </UnauthenticatedTemplate>
        </div>
    )
}
