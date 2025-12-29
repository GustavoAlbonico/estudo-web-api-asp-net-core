export interface LoginRequest {
  userName: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiration: string
  user: {
    id: number;
    userName: string;
    email: string;
  };
}