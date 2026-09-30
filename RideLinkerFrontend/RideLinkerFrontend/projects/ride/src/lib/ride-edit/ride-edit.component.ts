import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Trip, User, Car, Location } from '../../../../shared/src/public-api';
import { Subscription } from 'rxjs';
import { RideService } from '../ride.service';
import { CarService } from '@car/src/lib/car.service';
import { AuthService } from '../../../../shared/src/lib/auth/auth.service';
import { DateTime } from '@eonasdan/tempus-dominus';
import { state } from '@angular/animations';

@Component({
  selector: 'lib-ride-edit',
  templateUrl: './ride-edit.component.html',
  styleUrls: ['./ride-edit.component.css']
})
export class RideEditComponent implements OnInit, OnDestroy {
  ride: Trip | null = null;
  subscription: Subscription | undefined = undefined;
  id: string | null = null;
  locations: Location[] = [];
  cars: Car[] = [];
  departureCity: string | null = null;
  destinationCity: string | null = null;
  carForm: string | null = null;
  carCount: number = 0;
  errorMessageVT: string = '';
  errorMessageAT: string = '';

  departure: Location | null = null;
  destination: Location | null = null;
  startTime: Date = new Date();
  endTime: Date = new Date();
  startTimeFormatted: string = this.formatDate(this.startTime);
  endTimeFormatted: string = this.formatDate(this.endTime);

  driver: User | null = null;
  car: Car | null = null;

  constructor(private route: ActivatedRoute, public router: Router, private rideService: RideService, private authService: AuthService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id != null) {
      this.subscription = this.rideService.getRide(this.id).subscribe((results) => {
        this.ride = results;

        this.startTime = new Date(this.ride.startTime);
        this.endTime = new Date(this.ride.endTime);
        this.startTimeFormatted = this.formatDate(this.startTime);
        this.endTimeFormatted = this.formatDate(this.endTime);
      });
    }
    this.rideService.getLocations().subscribe(
      (locations) => {
        this.locations = locations;
      },
      (error) => {
        console.error('Error fetching locations:', error);
      }
    );
    this.rideService.getCars().subscribe(
      (cars) => {
        this.cars = cars;
      },
      (error) => {
        console.error('Error fetching cars:', error);
      }
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) this.subscription.unsubscribe();
  }

  get filteredCars(): Car[] {
    if (!this.departureCity) {
      return this.cars;
    } else {
      return this.cars.filter(car => car.location.name === this.departureCity);
    }
  }

  private formatDate(date: Date): string {
    const utcDate = new Date(date.getTime() - date.getTimezoneOffset() * 60000);
    return utcDate.toISOString().slice(0, 16);
  }

  onSubmit(): void {
    const currentDate = new Date();
    const startTimeDate = new Date(this.startTime);
    if (startTimeDate <= currentDate) {
      console.error('Vertrektijd moet in de toekomst liggen.');
      this.errorMessageVT = 'Vertrektijd moet in de toekomst liggen.';
    } else {
      this.errorMessageVT = '';
    }

    if (this.endTime <= this.startTime) {
      console.error('Aankomsttijd moet na de vertrektijd liggen.');
      this.errorMessageAT = 'Aankomsttijd moet na de vertrektijd liggen.';
    } else {
      this.errorMessageAT = '';
    }

    if (this.errorMessageVT || this.errorMessageAT) {
      return;
    }

    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id == null) {
      this.locations.forEach(location => {
        if (this.departureCity == location.name) {
          this.departure = location;
        }
      });
      this.locations.forEach(location => {
        if (this.destinationCity == location.name) {
          this.destination = location;
        }
      });
      this.cars.forEach(car => {
        let modelBrand = car.brand + " " + car.model;
        if (this.carForm == modelBrand) {
          this.car = car;
        }
      })

      this.rideService.createRide(this.departure?.id, this.destination?.id, this.startTime, this.endTime, this.car?.id).subscribe((results) => {
        console.log('creating');
        this.ride = results;
        this.router.navigate(['/trip']);
        this.rideService.joinRide(this.ride?.id, Number(this.authService.getDecodedToken().Id)).subscribe((results) => {
          console.log(`result: ${results}`);
        })
      })

    } else if (this.id != null) {
      this.locations.forEach(location => {
        if (this.ride?.departure.name == location.name) {
          this.ride.departure = location;
        }
        if (this.ride?.destination.name == location.name) {
          this.ride.destination = location;
        }

        if (this.ride) {
          this.startTime = this.ride.startTime;
          this.endTime = this.ride.endTime;
        }

      });
      this.cars.forEach(car => {
        let modelBrand = this.ride?.car.brand + " " + this.ride?.car.brand;
        if (this.carForm == modelBrand && this.ride?.car) {
          this.ride.car = car;
        }
      })

      this.rideService.updateRide(this.id, this.ride?.departure.id, this.ride?.destination.id, this.ride?.startTime, this.ride?.endTime, this.ride?.driver.id, this.ride?.car.id).subscribe((results) => {
        console.log(`updating ${this.id}`)
        console.log(`result: ${results}`);
        this.router.navigate(['/trip/' + this.id]);
      })
    }
  }
}
