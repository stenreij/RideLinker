import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { RideListComponent } from '../ride-list/ride-list.component';
import { RouterModule } from '@angular/router';
import { RideService } from '../ride.service';
import { NgxBootstrapIconsModule } from 'ngx-bootstrap-icons';
import { arrowCounterclockwise, arrowRight, flag, geoAlt, clock, carFront, person, caretDownSquare } from 'ngx-bootstrap-icons';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RideDetailsComponent } from '../ride-details/ride-details.component';
import { LoginComponent } from '@shared/src/lib/auth/login/login.component';
import { AuthService } from '@shared/src/lib/auth/auth.service';
import { SharedModule } from '@shared/src/public-api';
import { RideEditComponent } from '../ride-edit/ride-edit.component';
import { CarModule, CarService } from '@car/src/public-api';
import { RideAllComponent } from '../ride-all/ride-all.component';

const icons = {
  arrowCounterclockwise,
  arrowRight,
  flag,
  geoAlt,
  clock,
  carFront,
  person,
  caretDownSquare
};

@NgModule({
  declarations: [RideListComponent, RideDetailsComponent, RideEditComponent, RideAllComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgxBootstrapIconsModule.pick(icons),
    FormsModule,
    ReactiveFormsModule,
    CarModule
  ],
  providers: [RideService, { provide: LOCALE_ID, useValue: 'nl' }, CarService, AuthService],
  exports: [RideListComponent, RideDetailsComponent, RideEditComponent, RideAllComponent]

})
export class RideModule { }
