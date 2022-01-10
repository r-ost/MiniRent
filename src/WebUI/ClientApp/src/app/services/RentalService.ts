import { GetPriceQuery, PriceDto, RentalsClient } from "../web-api-client";

export interface IRentalService {
  getPrice: (
    accessToken: string,
    brand: string,
    model: string,
    location: string,
    rentDuration: number
  ) => Promise<PriceDto>;
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

    let priceDto = await rentalClient.getPrice(
      new GetPriceQuery({
        brand: brand,
        model: model,
        location: location,
        rentDuration: rentDuration,
      })
    );

    return priceDto;
  }
}
