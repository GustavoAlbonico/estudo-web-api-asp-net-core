export interface LoginRequest {
  userName: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  refreshToken: string,
  user: {
    id: number;
    userName: string;
    email: string;
  };
}