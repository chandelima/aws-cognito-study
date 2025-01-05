import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAuthData } from '../../interfaces/auth-data.interface';
import { IDefaultResponse } from '../../interfaces/default-response.interface';
import { BackendUrlService } from '../../services/backend-url.service';
import { TokenService } from './token.service';
import { IValue } from '../../interfaces/value.interface';
import { catchError, of, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _baseUrl!: string;

  constructor(
    private readonly _httpClient: HttpClient,
    private readonly _tokenService: TokenService,
    private readonly _backendUrlService: BackendUrlService,
    private readonly _router: Router
  ) {
    this.setBaseUrl();
  }

  private setBaseUrl() {
    this._baseUrl = this._backendUrlService.getUrl('/v1/auth');
  }

  get isAuthenticated() {
    return this._tokenService.isAccessTokenActive;
  }

  getLoginUrl() {
    const url = `${this._baseUrl}/get_login_url`;
    return this._httpClient.get<IDefaultResponse<IValue<string>>>(url);
  }

  completeLogin(authCode: string) {
    const payload = { value: authCode };
    const url = `${this._baseUrl}/get_token_from_auth_code`;

    return this._httpClient
      .post<IDefaultResponse<IAuthData>>(url, payload)
      .pipe(tap(res => {
        if (res?.data) {
          this._tokenService.authData = res.data;
          this._router.navigate(['']);
        }
      }));
  }

  refreshToken() {
    const url = `${this._baseUrl}/refresh_token`;
    const payload = { value: this._tokenService.refreshToken };

    return this._httpClient
      .post<IDefaultResponse<IAuthData>>(url, payload)
      .pipe(tap(res => {
        if (res?.data) {
          const authData = { ...this._tokenService.authData, ...res.data };
          this._tokenService.authData = authData;
        }
      }));
  }

  logout() {
    const url = `${this._baseUrl}/revoke_token`;
    const payload = { value: this._tokenService.refreshToken };

    if (this.isAuthenticated) {
      return this._httpClient
      .post<IDefaultResponse<IAuthData>>(url, payload)
      .pipe(tap(() => {
        this._tokenService.clear();
        window.location.href = window.location.origin;
      }));
    }

    return of();
  }
}
