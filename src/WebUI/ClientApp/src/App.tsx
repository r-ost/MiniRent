import React, { useState } from "react";
import { PageLayout } from "./components/PageLayout";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import { loginRequest, weatherForecastConfig } from "./authConfig";
import Button from "react-bootstrap/Button";
import { IWeatherForecast, IWeatherForecastClient, WeatherForecastClient } from "./app/web-api-client";
import { WeatherForecastData } from "./components/WeatherForecastData";
import { IWeatherForecastService, WeatherForecastService } from "./WeatherForecastService";



const WeatherForecastContent = (props: { weatherForecastService: IWeatherForecastService }) => {
  const { instance, accounts } = useMsal();
  const [weatherForecastData, setWeatherForecastData] = useState<IWeatherForecast | undefined>(undefined);

  const name = accounts[0] && accounts[0].name;

  function RequestProfileData() {
    const request = {
      ...loginRequest,
      account: accounts[0]
    };

    // Silently acquires an access token which is then attached to a request
    instance.acquireTokenSilent(request).then((response) => {
      props.weatherForecastService.getWeatherForecast(response.accessToken).then(response => setWeatherForecastData(response[0]));
    }).catch((e) => {
      instance.acquireTokenPopup(request).then((response) => {
        props.weatherForecastService.getWeatherForecast(response.accessToken).then(response => setWeatherForecastData(response[0]));
      });
    });
  }

  return (
    <>
      <h5 className="card-title">Welcome {name}</h5>
      {weatherForecastData ?
        <WeatherForecastData weatherForecastData={weatherForecastData} />
        :
        <Button variant="secondary" onClick={RequestProfileData}>Request Weather forecast</Button>
      }
    </>
  );
};


function App() {
  return (
    <PageLayout>
      <AuthenticatedTemplate>
        <WeatherForecastContent weatherForecastService={new WeatherForecastService()} />
      </AuthenticatedTemplate>
      <UnauthenticatedTemplate>
        <p>You are not signed in! Please sign in.</p>
      </UnauthenticatedTemplate>
    </PageLayout>
  );
}

export default App;
