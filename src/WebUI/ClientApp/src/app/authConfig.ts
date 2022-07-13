export const msalConfig = {
  auth: {
    clientId: "c1e0987a-56f2-4f3d-b13a-28edbc3682fe",
    authority:
      "https://login.microsoftonline.com/95580983-0258-4189-8b2a-01286b6d5df1", // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
    redirectUri: process.env.REDIRECT_URI,
  },
  cache: {
    cacheLocation: "sessionStorage", // This configures where your cache will be stored
    storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
  },
};

export const loginRequest = {
  scopes: ["api://42bcaff5-136a-4ff7-aba9-c30a15a429d0/access_as_user"],
};

// Add the endpoints
// TODO: change to environemnt variable
export const API_BASE_URL = "https://minirent.online";
