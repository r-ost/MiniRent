import { API_BASE_URL } from "../authConfig";
import {
  CurrentRentalDto,
  ReturnCarCommand,
  WorkerClient,
} from "../web-api-client";

export interface IWorkerService {
  returnCar: (
    accessToken: string,
    rentID: string,
    description: string,
    odometerValueInKm: number,
    overallState: string
  ) => Promise<number>;
  getCurrentRentals: (accessToken: string) => Promise<Array<CurrentRentalDto>>;
}

export class WorkerService implements IWorkerService {
  async returnCar(
    accessToken: string,
    rentID: string,
    description: string,
    odometerValueInKm: number,
    overallState: string
  ): Promise<number> {
    var client = new WorkerClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    let result = await client.returnCar(
      new ReturnCarCommand({
        rentId: rentID,
        description: description,
        odometerValueInKm: odometerValueInKm,
        overallState: overallState,
      })
    );

    return result;
  }

  async getCurrentRentals(
    accessToken: string
  ): Promise<Array<CurrentRentalDto>> {
    const client = new WorkerClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    let rentals = await client.getCurrentRentals();

    return rentals;
  }
}
