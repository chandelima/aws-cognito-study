import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../auth/services/auth.service';
import { TokenService } from '../auth/services/token.service';

let tokenService: TokenService;
let authService : AuthService

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  tokenService = inject(TokenService);
  authService = inject(AuthService);

  const accessToken = tokenService.accessToken;
  const requestWithToken = addAccessToken(req, accessToken);

  return next(requestWithToken).pipe(
    catchError(error => {
      if (error.status === 401) {
        // Try refresh token if receive 401
        return authService.refreshToken().pipe(
          switchMap((response) => {
            const newAccessToken = response.data.accessToken;
            const clonedReq = req.clone({
              setHeaders: {
                Authorization: `Bearer ${newAccessToken}`,
              },
            });

            return next(clonedReq);
          }),
          catchError((err) => {
            authService.logout();
            return throwError(() => new Error('Session expired', err));
          })
        );
      }
      return throwError(() => new Error(error));
    })
  );
};

const addAccessToken = (request: HttpRequest<any>, accessToken: string | null): HttpRequest<any> => {
  let modifiedRequest = request;

  if (accessToken) {
    modifiedRequest = request.clone({
      setHeaders: { Authorization: `Bearer ${accessToken}` },
    })
  }

  return modifiedRequest;
}
