import {
  HttpClient,
  type HttpErrorResponse,
  type HttpHandlerFn,
  type HttpInterceptorFn,
  type HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { removeCookieByKeyFromAllPaths } from '../Helpers/Helpers';
import { userDataCookieKey } from '../Constants/Constants';

export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  var httpClient = inject(HttpClient);

  return next(req).pipe(
    catchError((ex: HttpErrorResponse) => {
      if (ex.status == 401) {
        return httpClient
          .get('https://localhost:5999/api/v1/users/refresh-my-token', {
            withCredentials: true,
          })
          .pipe(
            switchMap((response: any) => {
              return next(req);
            }),
            catchError((refreshError: any) => {
              removeCookieByKeyFromAllPaths(userDataCookieKey);
              window.location.replace('/');
              // router
              //   .navigate(['/'], {
              //     replaceUrl: true,
              //   })
              //   .then(() => {
              //     location.reload();
              //   });
              return throwError(() => new Error(refreshError.message));
            })
          );
      } else return throwError(() => new Error(ex.message));
    })
  );
};
