import {
  CurrentRentalDto,
  GetPriceWithBrandAndModelQuery,
  HistoricRentalDto,
  PriceDto,
  RentalsClient,
  RentCarCommand,
  RentCarDto,
} from "../web-api-client";

export interface IRentalService {
  getPrice: (
    accessToken: string,
    brand: string,
    model: string,
    location: string,
    rentDuration: number
  ) => Promise<PriceDto>;

  rentCar: (
    accessToken: string,
    startDate: Date,
    endDate: Date,
    carId: string,
    location: string,
    rentCompany: string
  ) => Promise<RentCarDto>;

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
      "https://localhost:5001"
    );

    let rentals = await rentalClient.getHistoricRentals();

    return rentals;
  }

  async getPrice(
    accessToken: string,
    brand: string,
    model: string,
    location: string,
    rentDuration: number
  ): Promise<PriceDto> {
    const rentalClient = new RentalsClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      "https://localhost:5001"
    );

    let priceDto = await rentalClient.getPriceWithBrandAndModel(
      new GetPriceWithBrandAndModelQuery({
        brand: brand,
        model: model,
        location: location,
        rentDuration: rentDuration,
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
      "https://localhost:5001"
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

  async getCurrentRentals(
    accessToken: string
  ): Promise<Array<CurrentRentalDto>> {
    const rentalClient = new RentalsClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      "https://localhost:5001"
    );

    let rentals = await rentalClient.getCurrentRentals();

    return rentals;
  }
}
