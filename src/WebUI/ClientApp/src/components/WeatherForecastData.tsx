import React from "react";
import { IWeatherForecast } from "../app/web-api-client";


export const WeatherForecastData = (props: { weatherForecastData: IWeatherForecast }) => {
    return (
        <div id="profile-div">
            <p><strong>Date: </strong> {props.weatherForecastData.date?.toDateString()}</p>
            <p><strong>Summary: </strong> {props.weatherForecastData.summary}</p>
            <p><strong>TemperatureC: </strong> {props.weatherForecastData.temperatureC?.toString()}</p>
            <p><strong>TemperatureF: </strong> {props.weatherForecastData.temperatureF?.toString()}</p>
        </div>
    );
};