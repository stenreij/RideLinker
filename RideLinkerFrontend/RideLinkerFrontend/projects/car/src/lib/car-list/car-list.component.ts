import { Component, OnInit } from '@angular/core';
import { CarService } from '../car.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from '../../../../shared/src/lib/models/car.interface'

@Component({
  selector: 'lib-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent implements OnInit {
  cars: Car[] = [];

  constructor(private carService: CarService) {}

  ngOnInit(): void {
    console.log('CarListComponent ngOnInit');
    this.carService.getCars().subscribe(
      (cars) => {
        this.cars = cars;
      },
      (error) => {
        console.error('Error fetching cars:', error);
      }
    );
  }

}
