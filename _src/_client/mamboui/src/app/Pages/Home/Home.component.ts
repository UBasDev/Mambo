import { CommonModule } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { RouterModule } from '@angular/router';
import { setCookieByKey } from '../../Helpers/Helpers';
import {
  userDataCookieExpireTime,
  userDataCookieKey,
} from '../../Constants/Constants';
import { IUserModel } from '../../Models/User/UserModel';
import { IUserInitialState } from '../../StateStore/User/UserReducers';
import { Store } from '@ngrx/store';
import { UserStateActions } from '../../StateStore/User/UserActions';
import { catchError } from 'rxjs';

interface IComponentStates {
  isGetMeButtonActive: boolean;
}
interface IUserLogin {
  emailOrUsername: string;
  password: string;
}
interface IUserLoginResponse {
  errorMessage: string | null;
  isSuccessful: boolean;
  payload: {
    companyName: string;
    email: string;
    firstname: string;
    lastname: string;
    roleLevel: number;
    roleName: string;
    screens: ReadonlyArray<string>;
  };
  requestId: string;
  serverTimeout: number;
  statusCode: number;
}
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <p class="text-red-500 text-2xl">Home works!</p>
    <button (click)="this.login()">LOGIN</button>
    @if(this.componentStates.isGetMeButtonActive == true){
    <button (click)="this.getMyInfo()">GET MY INFO</button>
    }
  `,
  styleUrl: './Home.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent implements OnInit {
  constructor(
    private readonly _httpClient: HttpClient,
    private readonly userGlobalState: Store<{
      globalUserState: IUserInitialState;
    }>,
    private readonly _changeDetector: ChangeDetectorRef
  ) {}
  ngOnInit(): void {
    this.userGlobalState
      .select('globalUserState')
      .subscribe((data: IUserInitialState) => {
        if (data.loggedInUser != null) {
          this.componentStates = {
            ...this.componentStates,
            isGetMeButtonActive: true,
          };
        }
      });
  }
  componentStates: IComponentStates = {
    isGetMeButtonActive: false,
  };
  login(): void {
    var userData: IUserLogin = {
      emailOrUsername: 'ali1',
      password: 'Ali1Ali1Ali1',
    };
    this._httpClient
      .post<IUserLoginResponse>(
        'https://localhost:5999/api/v1/users/sign-in',
        userData,
        {
          withCredentials: true,
        }
      )
      .subscribe({
        next: (response: IUserLoginResponse) => {
          console.log('response', response);
          if (response.isSuccessful) {
            var userData: IUserModel = {
              email: response.payload.email,
              firstname: response.payload.firstname,
              lastname: response.payload.lastname,
              roleLevel: response.payload.roleLevel,
              roleName: response.payload.roleName,
              screens: response.payload.screens,
              username: response.payload.email,
            };
            this.userGlobalState.dispatch(
              UserStateActions.userLoggedIn({
                loggedInUser: userData,
              })
            );
            this.componentStates = {
              ...this.componentStates,
              isGetMeButtonActive: true,
            };
            this._changeDetector.markForCheck();
          }
        },
        error: (error: HttpErrorResponse) => {
          console.log('error response', error.error);
        },
      });
  }
  getMyInfo(): void {
    this.userGlobalState
      .select('globalUserState')
      .subscribe((data: IUserInitialState) => {
        console.log('user_data', data.loggedInUser);
      });
  }
}
