import { NgModule } from '@angular/core';
import { CarListComponent } from '../car-list/car-list.component';
import { CarService } from '../car.service';
import {RouterLink, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { CarDetailComponent } from '../car-detail/car-detail.component';
import { CarEditComponent } from '../car-edit/car-edit.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxBootstrapIconsModule } from 'ngx-bootstrap-icons';
import {carFront, geoAlt } from 'ngx-bootstrap-icons';

const icons = {
  carFront,
  geoAlt
};
@NgModule({
  declarations: [
    CarListComponent,
    CarDetailComponent
    
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgxBootstrapIconsModule.pick(icons),
    CarEditComponent,
    CommonModule,
    HttpClientModule
  ],
  providers: [CarService],
  exports: [CarListComponent, CarDetailComponent, CarEditComponent]
})
export class CarModule { }
