import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location, Reservation, Trip } from '@shared/src/public-api';
import { RideService } from '../ride.service';
import { startOfDay, differenceInMinutes, isValid } from 'date-fns';
import { AuthService } from '@shared/src/lib/auth/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'lib-ride-all',
  templateUrl: './ride-all.component.html',
  styleUrls: ['./ride-all.component.css']
})
export class RideAllComponent implements OnInit {
  rideForm: FormGroup;
  locations: Location[] = [];
  trips: Trip[] = [];
  reservations: Reservation[] = [];
  originalTrips: Trip[] = [];
  showFilterOptions: boolean = false;
  currentDate: string = '';
  numberOfReservationsPerRide: { [key: number]: number } = {}; // Object om het aantal reserveringen per rit bij te houden
  private reservationsSubscription: Subscription | undefined; // Abonnement op reserveringen

  constructor(
    private rideService: RideService,
    private fb: FormBuilder,
    private authService: AuthService,
  ) {
    this.currentDate = new Date().toISOString().slice(0, 10);
    this.rideForm = this.fb.group({
      departure: ['', Validators.required],
      destination: ['', Validators.required],
      dateTime: [new Date().toISOString().substring(0, 16)]
    });
  }

  ngOnInit(): void {
    console.log('Ride-AllComponent ngOnInit');
    this.currentDate = new Date().toISOString().slice(0, 10);
    this.rideForm.value.dateTime = new Date();
    this.loadLocations();
    this.loadReservations();
    this.resetFields();
  }

  loadLocations() {
    this.rideService.getLocations().subscribe(
      (locations) => {
        this.locations = locations;
        console.log(locations);
      },
      (error) => {
        console.error('Error fetching locations:', error);
      }
    );
  }

  loadReservations() {
    this.reservationsSubscription = this.rideService.getReservations().subscribe(
      (reservations) => {
        this.reservations = reservations;
        this.updateNumberOfReservations(); // Update het aantal reserveringen per rit wanneer nieuwe reserveringen binnenkomen
      },
      (error) => {
        console.error('Error fetching reservations:', error);
      }
    );
  }

  ngOnDestroy(): void {
    if (this.reservationsSubscription) {
      this.reservationsSubscription.unsubscribe(); // Unsubscribe om geheugenlekken te voorkomen
    }
  }

  updateNumberOfReservations() {
    // Reset het aantal reserveringen per rit
    this.numberOfReservationsPerRide = {};

    // Loop door alle reserveringen en tel het aantal per rit op
    this.reservations.forEach((reservation) => {
      const tripId = reservation.trip.id;
      this.numberOfReservationsPerRide[tripId] = (this.numberOfReservationsPerRide[tripId] || 0) + 1;
    });
  }

  loadNumberOFReservations(rideId: number): void {
    this.rideService.getNumberOfReservationsForRide(rideId).subscribe(
      (numberOfReservations) => {
        this.numberOfReservationsPerRide[rideId] = numberOfReservations;
      },
      (error) => {
        console.error('Error fetching number of reservations:', error);
      }
    );
  }

  resetFields() {
    this.setWhereToGo(false);
    this.loadTrips();
    this.rideForm.value.dateTime = new Date();
  }

  resetFilters() {
    const today = new Date().toISOString().substring(0, 10);
    const filterDateInput = document.getElementById('filterDate') as HTMLInputElement;
    filterDateInput.value = today;
    this.rideForm.get('dateTime')?.setValue(today);
    this.resetDeparture();
    this.resetDestination();
    this.loadTrips();
  }

  loadTrips() {
    this.rideService.getRides().subscribe(
      (trips) => {
        const today = startOfDay(new Date());
        this.originalTrips = trips;
        this.trips = trips
          .filter(trip => new Date(trip.startTime) >= today)
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

  selectDeparture(location: Location) {
    let departure = this.rideForm.get('departure')
    departure?.setValue(location);
    this.onSubmit();
  }

  selectDestination(location: Location) {
    let destination = this.rideForm.get('destination')
    destination?.setValue(location);
    this.onSubmit();
  }

  resetDeparture() {
    this.rideForm.get('departure')?.setValue('');
  }

  resetDestination() {
    this.rideForm.get('destination')?.setValue('');
  }

  applyDateFilter() {
    const filterDate = (document.getElementById('filterDate') as HTMLInputElement).value;
    const dateTimeControl = this.rideForm.get('dateTime');

    if (filterDate) {
      // Als de filterdatum is ingesteld, pas de waarde van de dateTime-control aan
      dateTimeControl?.setValue(filterDate);
    }

    // Roep de onSubmit-methode alleen aan als er een filterdatum is ingesteld
    if (filterDate || dateTimeControl?.value) {
      this.onSubmit();
    }
  }

  onSubmit() {
    const formValue = this.rideForm.value;
    const departureId = formValue.departure ? formValue.departure.id : null;
    const destinationId = formValue.destination ? formValue.destination.id : null;
    const dateTime = formValue.dateTime ? formValue.dateTime : new Date().toISOString().substring(0, 16);

    this.setWhereToGo(true);
    console.log('Form Data:', formValue);

    // Converteer de dateTime van het formulier naar het begin van de dag voor vergelijking
    const searchDate = startOfDay(new Date(dateTime));
    if (!formValue.dateTime || !isValid(searchDate)) {
      console.error('Ongeldige datum opgegeven');
      return;
    }

    this.rideService.getRides().subscribe(
      (trips) => {
        // Filteren op basis van vertrek- en bestemmingslocatie
        this.trips = trips.filter((trip: Trip) => {
          const matchesDeparture = !departureId || trip.departure.id === departureId;
          const matchesDestination = !destinationId || trip.destination.id === destinationId;
          return matchesDeparture && matchesDestination;
        });

        // Filteren op basis van de geselecteerde datum
        if (isValid(searchDate)) {
          this.trips = this.trips.filter((trip: Trip) => {
            const tripDate = startOfDay(new Date(trip.startTime));
            return tripDate >= searchDate; // Alleen ritten vanaf de opgegeven datum
          });
        }

        // Sorteren op basis van het verschil in tijd met de opgegeven datum
        this.trips.sort((a: Trip, b: Trip) => {
          const timeDiffA = differenceInMinutes(new Date(a.startTime), new Date(dateTime));
          const timeDiffB = differenceInMinutes(new Date(b.startTime), new Date(dateTime));
          return Math.abs(timeDiffA) - Math.abs(timeDiffB);
        });
      },
      (error) => {
        console.error('Fout bij ophalen van ritten:', error);
      }
    );
  }

  rotateArrow() {
    let arrow = document.getElementById("searchExpand");
    if (arrow?.style.transform == "rotate(180deg)") {
      arrow.style.transform = "rotate(0deg)";
      let tripList = document.getElementById("tripList");
      if (tripList)
        tripList.style.height = "60vh";
    } else {
      if (arrow)
        arrow.style.transform = "rotate(180deg)";
      let tripList = document.getElementById("tripList");
      if (tripList)
        tripList.style.height = "60vh";
    }
  }

  setWhereToGo(bool: boolean) {
    let WTG = document.getElementById("WTG");
    if (bool) {
      if (WTG)
        WTG.innerHTML = "Waar wil je heen?";
    } else {
      if (WTG)
        WTG.innerHTML = "Geplande ritten";
    }
  }

  getNumberOfReservations(tripId: number) {
    return this.numberOfReservationsPerRide[tripId] || 0; // Return het aantal reserveringen per rit, of 0 als het niet is bijgehouden
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
