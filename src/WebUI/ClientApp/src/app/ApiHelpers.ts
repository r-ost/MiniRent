import { AccountInfo, IPublicClientApplication } from "@azure/msal-browser";
import { loginRequest } from "./authConfig";

export const callApiAuthenticated = (
  instance: IPublicClientApplication,
  account: AccountInfo,
  callback: (accessToken: string) => void
) => {
  const request = {
    ...loginRequest,
    account: account,
  };

  instance
    .acquireTokenSilent(request)
    .then((response) => {
      callback(response.accessToken);
    })
    .catch((e) => {
      instance.acquireTokenPopup(request).then((response) => {
        callback(response.accessToken);
      });
    });
};
