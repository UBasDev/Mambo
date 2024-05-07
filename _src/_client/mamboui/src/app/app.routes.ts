import { Routes } from '@angular/router';
import { CommonLayoutComponent } from './Layouts/CommonLayout/CommonLayout.component';
import { HomeComponent } from './Pages/Home/Home.component';

export const routes: Routes = [
  {
    path: '',
    component: CommonLayoutComponent,
    children: [
      {
        path: '',
        component: HomeComponent,
      },
    ],
  },
];
