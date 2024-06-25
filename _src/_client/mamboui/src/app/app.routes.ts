import { Routes } from '@angular/router';
import { CommonLayoutComponent } from './Layouts/CommonLayout/CommonLayout.component';
import { HomeComponent } from './Pages/Home/Home.component';
import { Authorized1Component } from './Pages/Authorized1/Authorized1.component';
import { AuthGuard } from './Guards/Auth.guard';
import { Unauthorized1Component } from './Pages/Unauthorized1/Unauthorized1.component';
import { SignUpComponent } from './Pages/SignUp/SignUp.component';

export const routes: Routes = [
  {
    path: '',
    component: CommonLayoutComponent,
    children: [
      {
        path: '',
        component: HomeComponent,
      },
      {
        path: 'authorized1',
        component: Authorized1Component,
        canActivate: [AuthGuard],
      },
      {
        path: 'unauthorized1',
        component: Unauthorized1Component,
      },
    ],
  },
  {
    path: 'signup',
    component: SignUpComponent,
  },
];
