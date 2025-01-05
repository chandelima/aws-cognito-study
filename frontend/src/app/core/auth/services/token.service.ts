import { Injectable } from '@angular/core';
import { jwtDecode } from "jwt-decode";
import { IAuthData } from '../../interfaces/auth-data.interface';

const LOCALSTORAGE_AUTH_DATA_KEY = "auth_data"

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  get authData(): IAuthData | null {
    const stringData = localStorage.getItem(LOCALSTORAGE_AUTH_DATA_KEY);

    if (!stringData) return null

    const data = JSON.parse(stringData);
    return data;
  }

  set authData(data: IAuthData | null) {
    if (!data) return;

    const stringData = JSON.stringify(data);
    localStorage.setItem(LOCALSTORAGE_AUTH_DATA_KEY, stringData);
  }

  get accessToken(): string | null {
    return this.authData?.accessToken ?? null;
  }

  get isAccessTokenActive(): boolean {
    const { exp } = this.decodeToken(this.accessToken) || {};
    const now = Math.floor(Date.now() / 1000);

    return !!exp && exp > now;
  }

  get refreshToken(): string | null {
    return this.authData?.refreshToken ?? null;
  }

  public clear() {
    localStorage.removeItem(LOCALSTORAGE_AUTH_DATA_KEY);
  }

  private decodeToken(token: string | null) {
    try {
      return jwtDecode(token as string);
    } catch {
      return null;
    }
  }
}
