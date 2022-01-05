import React, { useState } from "react";
import { PageLayout } from "../PageLayout/PageLayout";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useIsAuthenticated, useMsal } from "@azure/msal-react";
import { loginRequest } from "../../app/authConfig";
import Button from "react-bootstrap/Button";
import { IWeatherForecast } from "../../app/web-api-client";
import { WeatherForecastData } from "../WeatherForecastData";
import { IWeatherForecastService, WeatherForecastService } from "../../app/services/WeatherForecastService";
import { Route, RouteProps, Routes, Navigate } from "react-router-dom";
import { MainPage } from "../../pages/Main/MainPage";
import { RegisterPage } from "../../pages/Register/RegisterPage";
import { Error404Page } from "../../pages/Error404/Error404Page";
import { OffersPage } from "../../pages/Offers/OffersPage";
import { VehiclesService } from "../../app/services/VehiclesService";



// const WeatherForecastContent = (props: { weatherForecastService: IWeatherForecastService }) => {
//   const { instance, accounts } = useMsal();
//   const [weatherForecastData, setWeatherForecastData] = useState<IWeatherForecast | undefined>(undefined);

//   const name = accounts[0] && accounts[0].name;

//   function RequestProfileData() {
//     const request = {
//       ...loginRequest,
//       account: accounts[0]
//     };

//     // Silently acquires an access token which is then attached to a request
//     instance.acquireTokenSilent(request).then((response) => {
//       props.weatherForecastService.getWeatherForecast(response.accessToken).then(response => setWeatherForecastData(response[0]));
//     }).catch((e) => {
//       instance.acquireTokenPopup(request).then((response) => {
//         props.weatherForecastService.getWeatherForecast(response.accessToken).then(response => setWeatherForecastData(response[0]));
//       });
//     });
//   }

//   return (
//     <>
//       <h5 className="card-title">Welcome {name}</h5>
//       {weatherForecastData ?
//         <WeatherForecastData weatherForecastData={weatherForecastData} />
//         :
//         <Button variant="secondary" onClick={RequestProfileData}>Request Weather forecast</Button>
//       }
//     </>
//   );
// };



function App() {
  return (
    <div>

      <Routes>
        <Route path="/" element={<PageLayout />}>
          <Route index element={<MainPage />}></Route>
          <Route path="register" element={<RegisterPage />}></Route>
          <Route path="offers" element={
            <div>
              <AuthenticatedTemplate>
                <OffersPage vehiclesService={new VehiclesService()} />
              </AuthenticatedTemplate>
              <UnauthenticatedTemplate>
                <Navigate to="/"></Navigate>
              </UnauthenticatedTemplate>
            </div>
          }></Route>
          <Route path="*" element={<Error404Page />}></Route>
        </Route>
      </Routes>


    </div>
  );
}

export default App;