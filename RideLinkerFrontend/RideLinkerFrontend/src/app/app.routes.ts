import { Routes } from '@angular/router';
import { CarListComponent } from '@car/src/lib/car-list/car-list.component';
import { CarDetailComponent, CarEditComponent } from '@car/src/public-api';
import { RideEditComponent } from '@ride/src/lib/ride-edit/ride-edit.component';
import { RideDetailsComponent, RideListComponent } from '@ride/src/public-api';
import { AuthGuard } from '@shared/src/lib/auth/auth.guard';
import { LoginComponent } from '@shared/src/lib/auth/login/login.component';
import { RegisterComponent } from '@shared/src/lib/auth/register/register.component';
import { RideAllComponent } from '@ride/src/lib/ride-all/ride-all.component';

export const routes: Routes = [

  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'login',
  },
  {
    path: 'login',
    pathMatch: 'full',
    component: LoginComponent
  },
  {
    path: 'register',
    pathMatch: 'full',
    component: RegisterComponent
  },
  {
    path: 'home',
    pathMatch: 'full',
    component: RideAllComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'car/:id',
    pathMatch: 'full',
    component: CarDetailComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'car/:id/edit',
    pathMatch: 'full',
    component: CarEditComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'car/:id/create',
    pathMatch: 'full',
    component: CarEditComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'car',
    pathMatch: 'full',
    component: CarListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'trip',
    pathMatch: 'full',
    component: RideListComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'trip/new',
    pathMatch: 'full',
    component: RideEditComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'trip/:id',
    pathMatch: 'full',
    component: RideDetailsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'trip/:id/edit',
    pathMatch: 'full',
    component: RideEditComponent,
    canActivate: [AuthGuard],
  },
  {
    path: '**',
    pathMatch: 'full',
    redirectTo: 'login',
  }
];
