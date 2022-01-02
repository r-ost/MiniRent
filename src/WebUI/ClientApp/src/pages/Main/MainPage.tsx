import { IPublicClientApplication } from "@azure/msal-browser";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react"
import { LoginScreen } from "../../components/LoginScreen/LoginScreen"
import { loginRequest } from "../../app/authConfig";

export const MainPage: React.FC = () => {
    function handleLogin(instance: IPublicClientApplication) {
        instance.loginRedirect(loginRequest).catch(e => {
            console.error(e);
        });
    }

    const { instance } = useMsal();


    return (
        <div>
            <AuthenticatedTemplate>
                <h1>Authenticated!</h1>
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                <LoginScreen handleLogin={() => handleLogin(instance)}></LoginScreen>
            </UnauthenticatedTemplate>
        </div>
    )
}
