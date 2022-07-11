import { API_BASE_URL } from "../authConfig";
import {
  CurrentRentalDto,
  GetPriceWithBrandAndModelQuery,
  HistoricRentalDto,
  PriceDto,
  RentalsClient,
  RentCarCommand,
  RentCarDto,
  ReturnCarCommand,
  WorkerClient,
} from "../web-api-client";

export interface IRentalService {
  getPrice: (
    accessToken: string,
    brand: string,
    model: string,
    location: string,
    rentDuration: number,
    company: string
  ) => Promise<PriceDto>;

  rentCar: (
    accessToken: string,
    startDate: Date,
    endDate: Date,
    carId: string,
    location: string,
    rentCompany: string
  ) => Promise<RentCarDto>;

  returnCar: (accessToken: string, rentId: string, company: string, description: string, odometerValueInKm: number, overallState: string) => Promise<number>;

  getCurrentRentals: (accessToken: string) => Promise<Array<CurrentRentalDto>>;

  getHistoricRentals: (
    accessToken: string
  ) => Promise<Array<HistoricRentalDto>>;

  // returnCar: (accessToken: string, rentID: string) => Promise<number>;
}

export class RentalService implements IRentalService {
  async getHistoricRentals(
    accessToken: string
  ): Promise<Array<HistoricRentalDto>> {
    const rentalClient = new RentalsClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    let rentals = await rentalClient.getHistoricRentals();

    return rentals;
  }

  async getPrice(
    accessToken: string,
    brand: string,
    model: string,
    location: string,
    rentDuration: number,
    company: string
  ): Promise<PriceDto> {
    const rentalClient = new RentalsClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    let priceDto = await rentalClient.getPriceWithBrandAndModel(
      new GetPriceWithBrandAndModelQuery({
        brand: brand,
        model: model,
        location: location,
        rentDuration: rentDuration,
        company: company,
      })
    );

    return priceDto;
  }

  async rentCar(
    accessToken: string,
    startDate: Date,
    endDate: Date,
    carId: string,
    location: string,
    rentCompany: string
  ): Promise<RentCarDto> {
    const rentalClient = new RentalsClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    let priceDto = await rentalClient.rentCar(
      new RentCarCommand({
        startDate: startDate,
        endDate: endDate,
        carId: carId,
        location: location,
        rentCompany: rentCompany,
      })
    );

    return priceDto;
  }

  async returnCar(accessToken: string, rentId: string, company: string, description: string, odometerValueInKm: number, overallState: string): Promise<number>{
    const workerClient = new WorkerClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    let result = await workerClient.returnCar(
      new ReturnCarCommand({
        company: company,
        description: description,
        odometerValueInKm: odometerValueInKm,
        overallState: overallState,
        rentId: rentId
      })
    );

    return result;
  }

  async getCurrentRentals(
    accessToken: string
  ): Promise<Array<CurrentRentalDto>> {
    const rentalClient = new RentalsClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    let rentals = await rentalClient.getCurrentRentals();

    return rentals;
  }
}
