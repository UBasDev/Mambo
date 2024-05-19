import { createReducer, on } from '@ngrx/store';
import {
  getUserDataFromCookieAndParse,
  removeCookieByKeyFromAllPaths,
  setCookieByKey,
} from '../../Helpers/Helpers';
import { IUserModel } from '../../Models/User/UserModel';
import { UserStateActions } from './UserActions';
import {
  userDataCookieExpireTime,
  userDataCookieKey,
} from '../../Constants/Constants';

let userDataFromLocalStorage: IUserModel | null =
  getUserDataFromCookieAndParse();
export interface IUserInitialState {
  loggedInUser: IUserModel | null;
}
export const UserInitialState: IUserInitialState = {
  loggedInUser: userDataFromLocalStorage,
};
export const GlobalUserReducer = createReducer(
  UserInitialState,
  on(UserStateActions.userLoggedIn, (_state, payload) => {
    setCookieByKey(
      userDataCookieKey,
      JSON.stringify(payload.loggedInUser),
      userDataCookieExpireTime
    );
    return {
      loggedInUser: payload.loggedInUser,
    };
  }),
  on(UserStateActions.userLoggedOut, (_state) => {
    removeCookieByKeyFromAllPaths(userDataCookieKey);
    return {
      loggedInUser: null,
    };
  })
);
