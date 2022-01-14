import {
  CurrentRentalDto,
  GetPriceWithBrandAndModelQuery,
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
    quoteId: string,
    startDate: Date,
    carId: string
  ) => Promise<RentCarDto>;

  getCurrentRentals: (accessToken: string) => Promise<Array<CurrentRentalDto>>;

  returnCar: (accessToken: string, rentID: string) => Promise<number>;
}

export class RentalService implements IRentalService {
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
    quoteId: string,
    startDate: Date,
    carId: string
  ): Promise<RentCarDto> {
    const rentalClient = new RentalsClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      "https://localhost:5001"
    );

    let priceDto = await rentalClient.rentCar(
      new RentCarCommand({
        quoteId: quoteId,
        startDate: startDate,
        carId: carId,
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

  async returnCar(accessToken: string, rentID: string): Promise<number> {
    const rentalClient = new RentalsClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      "https://localhost:5001"
    );

    let result = await rentalClient.returnCar(rentID);

    return result;
  }
}
