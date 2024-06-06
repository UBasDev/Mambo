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
import { IUserInitialState } from '../StateStore/User/UserReducers';
import { Store } from '@ngrx/store';
import { UserStateActions } from '../StateStore/User/UserActions';
import { IUserLoginResponse } from '../Pages/Home/Home.component';
import { IUserModel } from '../Models/User/UserModel';

export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  const httpClient = inject(HttpClient);
  const userGlobalState = inject(Store<{ globalUserState: IUserInitialState }>);
  return next(req).pipe(
    catchError((ex: HttpErrorResponse) => {
      if (ex.status == 401) {
        return httpClient
          .get<IUserLoginResponse>(
            'https://localhost:5999/api/v1/users/refresh-my-token',
            {
              withCredentials: true,
            }
          )
          .pipe(
            switchMap((response: IUserLoginResponse) => {
              const userData: IUserModel = {
                email: response.payload.email,
                firstname: response.payload.firstname,
                lastname: response.payload.lastname,
                roleLevel: response.payload.roleLevel,
                roleName: response.payload.roleName,
                screens: response.payload.screens,
                username: response.payload.email,
              };
              userGlobalState.dispatch(
                UserStateActions.userLoggedIn({
                  loggedInUser: userData,
                })
              );
              return next(req);
            }),
            catchError((refreshError: any) => {
              removeCookieByKeyFromAllPaths(userDataCookieKey);
              window.location.replace('/');
              return throwError(() => new Error(refreshError.message));
            })
          );
      } else
        return throwError(() => {
          removeCookieByKeyFromAllPaths(userDataCookieKey);
          window.location.replace('/');
          return new Error(ex.message);
        });
    })
  );
};
