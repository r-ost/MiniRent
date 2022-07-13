export class IConfig {
    readonly bearerToken!: string;
}

export class AuthorizedApiBase {
  private readonly config: IConfig;

  protected constructor(config: IConfig) {
    this.config = config;
    }


  protected transformOptions = (options: RequestInit): Promise<RequestInit> => {
    options.headers = {
        ...options.headers,
        Authorization: this.config.bearerToken,
    };
    return Promise.resolve(options);
  };
}