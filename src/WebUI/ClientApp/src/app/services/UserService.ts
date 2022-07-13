import { API_BASE_URL } from "../authConfig";
import { CreateUserCommand, UserClient, UserDetailsDto } from "../web-api-client";

export interface IUserService {
  userExists: (token: string) => Promise<boolean>;
  createUser: (dto: CreateUserCommand, accessToken: string) => void;
  getUser: (accessToken: string) => Promise<UserDetailsDto>;
}

export class UserService implements IUserService {
  async getUser(accessToken: string): Promise<UserDetailsDto>{
    const userClient = new UserClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    return await userClient.getUserDetails();
  }


  async userExists(accessToken: string): Promise<boolean> {
    const userClient = new UserClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    let userExist = false;
    await userClient
      .getUserDetails()
      .then(() => {
        userExist = true;
      })
      .catch((err) => {
        userExist = false;
      });

    return userExist;
  }

  async createUser(dto: CreateUserCommand, accessToken: string) {
    const userClient = new UserClient(
      {
        bearerToken: `Bearer ${accessToken}`,
      },
      API_BASE_URL
    );

    await userClient.create(dto);
  }
}
