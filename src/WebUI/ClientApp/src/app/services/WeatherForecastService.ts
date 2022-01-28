import { API_BASE_URL } from "../authConfig";
import {
  IConfig,
  IWeatherForecast,
  WeatherForecastClient,
} from "../web-api-client";

export interface IWeatherForecastService {
  getWeatherForecast: (token: string) => Promise<IWeatherForecast[]>;
}

export class WeatherForecastService implements IWeatherForecastService {
  getWeatherForecast(accessToken: string): Promise<IWeatherForecast[]> {
    const weatherForecastClient = new WeatherForecastClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    return weatherForecastClient.get();
  }
}
