import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Car } from '../../../../shared/src/lib/models/car.interface';
import { CarService } from '../car.service';

@Component({
  selector: 'lib-car-detail',
  templateUrl: './car-detail.component.html',
  styleUrls: ['./car-detail.component.css']
})
export class CarDetailComponent implements OnInit {
  car: Car | undefined;

  constructor(
    private route: ActivatedRoute,
    private carService: CarService
  ) {}

  ngOnInit(): void {
    this.loadCarDetails();
  }

  private loadCarDetails(): void {
    const carId = this.getCarIdFromRoute();
    
    // Subscribe to the observable
    this.carService.getCar(carId).subscribe(
      (car) => {
        this.car = car;
      },
      (error) => {
        console.error('Error fetching car details:', error);
      }
    );
  }

  public getCarIdFromRoute(): number {
    const idParam = this.route.snapshot.paramMap.get('id');
    console.log('idParam', idParam);
    if (!idParam || idParam == null) {
      throw new Error('Car ID not provided in the route');
    }
    return +idParam; 
  }
}
