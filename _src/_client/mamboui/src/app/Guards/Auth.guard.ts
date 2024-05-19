import { inject } from '@angular/core';
import {
  Router,
  type ActivatedRouteSnapshot,
  type CanActivateFn,
  type RouterStateSnapshot,
} from '@angular/router';
import { Store } from '@ngrx/store';
import { IUserInitialState } from '../StateStore/User/UserReducers';
import { IUserModel } from '../Models/User/UserModel';
import { map, Observable, take } from 'rxjs';

export const AuthGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
): Observable<boolean> => {
  const userState = inject(Store<{ globalUserState: IUserInitialState }>);
  const router = inject(Router);
  return userState.select('globalUserState').pipe(
    take(1),
    map((data: IUserInitialState) => {
      const userData: IUserModel | null = data.loggedInUser;
      if (userData == null) {
        router.navigate(['/']);
        return false;
      }
      return userData != null;
    })
  );
};
