export const msalConfig = {
  auth: {
    clientId: "c1e0987a-56f2-4f3d-b13a-28edbc3682fe",
    authority: "https://login.microsoftonline.com/common", // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
    redirectUri: "https://localhost:5001/",
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
export const weatherForecastConfig = {
  weatherForecastEndpoint: "https://localhost:5001/WeatherForecast",
};
