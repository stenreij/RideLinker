import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Location, Reservation, Trip } from '../../../../shared/src/public-api';  
import { RideService } from '../ride.service';
import { startOfDay, isSameDay, differenceInMinutes, isValid, set } from 'date-fns';
import { AuthService } from '../../../../shared/src/lib/auth/auth.service';

@Component({
  selector: 'lib-ride-list',
  templateUrl: './ride-list.component.html',
  styleUrls: ['./ride-list.component.css']
})
export class RideListComponent implements OnInit {
  rideForm: FormGroup;
  locations: Location[] = [];
  trips: Trip[] = [];
  reservations: Reservation[] = [];
  originalTrips: Trip[] = [];
  showFilterOptions: boolean = false;
  currentDate: string = '';

  constructor(
    private rideService: RideService,
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.rideForm = this.fb.group({
      departure: ['', Validators.required],
      destination: ['', Validators.required],
      dateTime: [new Date().toISOString().substring(0, 16)]
    });
  }

  ngOnInit(): void {
    console.log('RideListComponent ngOnInit');
    this.currentDate = new Date().toISOString().slice(0, 10);
    this.rideForm.value.dateTime = new Date();
    console.log(this.authService.getToken());
    this.rideService.getLocations().subscribe(
      (locations) => {
        this.locations = locations;
      },
      (error) => {
        console.error('Error fetching locations:', error);
      }
    );
    this.loadUserRides();
    this.resetFields();
  }

  resetFields() {
    this.setWhereToGo(false);
    this.loadUserRides();
    this.rideForm.reset();
  }

  applyDateFilter() {
    const filterDate = (document.getElementById('filterDate') as HTMLInputElement).value;
    if (filterDate) {
      const filteredDate = new Date(filterDate);
      this.trips = this.originalTrips.filter(trip => new Date(trip.startTime) >= filteredDate);
      this.trips.sort((a, b) => {
        const startTimeA = new Date(a.startTime).getTime();
        const startTimeB = new Date(b.startTime).getTime();
        return startTimeA - startTimeB;
      });
    } else {
      this.trips = [...this.originalTrips];
    }
  }  

  resetFilters() {
    const today = new Date().toISOString().substring(0, 10);
    const filterDateInput = document.getElementById('filterDate') as HTMLInputElement;
    filterDateInput.value = today; 
    this.applyDateFilter();
  }
  
  loadUserRides() {
    this.rideService.getUserRides(Number(this.authService.getDecodedToken().Id)).subscribe(
      (trips) => {
        const today = startOfDay(new Date());
        this.originalTrips = trips;
        this.trips = trips
          .filter(trip => startOfDay(new Date(trip.startTime)) >= today)
          .sort((a, b) => {
            const startTimeA = new Date(a.startTime).getTime();
            const startTimeB = new Date(b.startTime).getTime();
            return startTimeA - startTimeB;
          });
      },
      (error) => {
        console.error('Error fetching rides:', error);
      }
    );
  }
  
  onSubmit() {
    if (this.rideForm.valid) {
    this.setWhereToGo(true);
      console.log('Form Data:', this.rideForm.value);
      const formValue = this.rideForm.value;
      if (!formValue.dateTime) {
        formValue.dateTime = new Date();
      }
      let searchDate = startOfDay(new Date(formValue.dateTime));
      if (!formValue.dateTime || !isValid(searchDate)) {
        searchDate = new Date(); 
        console.error('Invalid date provided');
      }
      this.rideService.getRides().subscribe(
        (trips) => {
          this.trips = trips.filter((trip: Trip) => {
            return trip.departure.id === formValue.departure.id &&
              trip.destination.id === formValue.destination.id &&
              isSameDay(new Date(trip.startTime), searchDate);
          }).sort((a: Trip, b: Trip) => {
            const timeDiffA = differenceInMinutes(new Date(a.startTime), new Date(formValue.dateTime));
            const timeDiffB = differenceInMinutes(new Date(b.startTime), new Date(formValue.dateTime));
            return Math.abs(timeDiffA) - Math.abs(timeDiffB);
          });
        },
        (error) => {
          console.error('Error fetching rides:', error);
        }
      );
    }
  }

  rotateArrow(){
    let arrow = document.getElementById("searchExpand");
    if (arrow?.style.transform == "rotate(180deg)"){
      arrow.style.transform = "rotate(0deg)";
      let tripList = document.getElementById("tripList");
      if(tripList)
      tripList.style.height = "75vh";
    } else {
      if(arrow)
      arrow.style.transform = "rotate(180deg)";
      let tripList = document.getElementById("tripList");
      if(tripList)
      tripList.style.height = "55vh";
    }
  }

  setWhereToGo(bool: boolean){
    let WTG = document.getElementById("WTG");
    if(bool){
      if(WTG)
      WTG.innerHTML = "Waar wil je heen?";
    } else {
      if(WTG)
      WTG.innerHTML = "Mijn Ritten";
    }
  }

  toggleFilterOptions() {
    const filterOptions = document.getElementById('filterOptions');
    if (filterOptions) {
      this.showFilterOptions = !this.showFilterOptions;
      console.log("showFilterOptions:", this.showFilterOptions); 
      if (this.showFilterOptions) {
        filterOptions.style.maxHeight = '75px'; 
      } else {
        filterOptions.style.maxHeight = '0px'; 
      }
    }
  }
}  