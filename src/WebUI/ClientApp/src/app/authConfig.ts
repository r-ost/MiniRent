export const msalConfig = {
  auth: {
    clientId: process.env.REACT_APP_CLIENT_ID ?? "",
    authority:
      "https://login.microsoftonline.com/95580983-0258-4189-8b2a-01286b6d5df1", 
  },
  cache: {
    cacheLocation: "sessionStorage", // This configures where your cache will be stored
    storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
  },
};

export const loginRequest = {
  scopes: ["api://42bcaff5-136a-4ff7-aba9-c30a15a429d0/access_as_user"],
};


export const API_BASE_URL = process.env.REACT_APP_API_BASE_URL ?? "";
