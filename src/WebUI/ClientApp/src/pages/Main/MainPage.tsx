import { IPublicClientApplication } from "@azure/msal-browser";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react"
import { LoginScreen } from "../../components/LoginScreen/LoginScreen"
import { loginRequest } from "../../app/authConfig";
import { SearchPanel } from "../../components/SearchPanel/SearchPanel";

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
                <SearchPanel></SearchPanel>
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                <LoginScreen handleLogin={() => handleLogin(instance)}></LoginScreen>
            </UnauthenticatedTemplate>
        </div>
    )
}
