import { Routes } from '@angular/router';
import { CommonLayoutComponent } from './Layouts/CommonLayout/CommonLayout.component';
import { HomeComponent } from './Pages/Home/Home.component';
import { Authorized1Component } from './Pages/Authorized1/Authorized1.component';
import { AuthGuard } from './Guards/Auth.guard';

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
    ],
  },
];
