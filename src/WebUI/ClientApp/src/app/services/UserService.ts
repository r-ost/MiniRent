import { API_BASE_URL } from "../authConfig";
import { CreateUserCommand, UserClient } from "../web-api-client";

export interface IUserService {
  userExists: (token: string) => Promise<boolean>;
  createUser: (dto: CreateUserCommand, accessToken: string) => void;
}

export class UserService implements IUserService {
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
