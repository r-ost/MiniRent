import { API_BASE_URL } from "../authConfig";
import { IVehicleDto, VehiclesClient } from "../web-api-client";

export interface IVehiclesService {
  getVehicles: (token: string) => Promise<IVehicleDto[]>;
}

export class VehiclesService implements IVehiclesService {
  getVehicles(accessToken: string): Promise<IVehicleDto[]> {
    const weatherForecastClient = new VehiclesClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    return weatherForecastClient.get();
  }
}
