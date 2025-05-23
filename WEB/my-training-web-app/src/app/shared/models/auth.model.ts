export interface Credentials {
  username: string;
  password: string;
}

export interface AuthResponse {
  message: string;
  token: string;
  refreshToken?: string;
  user?: {
    id: string;
    userName: string;
    email: string;
    firstName: string;
    lastName: string;
  };
}
