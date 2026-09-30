import { Component, OnDestroy, OnInit } from '@angular/core';
import { Car, Reservation, Trip, User } from '@shared/src/public-api';
import { RideService } from '../ride.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, forkJoin } from 'rxjs';
import { AuthService } from '../../../../shared/src/lib/auth/auth.service';

@Component({
  selector: 'lib-ride-details',
  templateUrl: './ride-details.component.html',
  styleUrls: ['./ride-details.component.css']
})
export class RideDetailsComponent implements OnInit, OnDestroy {
  ride: Trip | null = null;
  subscription: Subscription | undefined = undefined;
  id: string | null = null;
  numberOfReservations: number = 0;
  reservations: Reservation[] = [];
  reservation: Reservation | null = null;
  riders: User[] = [];
  rider: User | null = null;

  constructor(private route: ActivatedRoute, private router: Router, private rideService: RideService, private authService: AuthService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(this.id);

    forkJoin({
      reservations: this.rideService.getReservations(),
      ride: this.rideService.getRide(this.id),
      numberOfReservations: this.rideService.getNumberOfReservationsForRide(Number(this.id))
    }).subscribe(({ reservations, ride, numberOfReservations }) => {
      console.log(`reservations: ${reservations}`);
      console.log(`ride: ${ride}`);
      console.log(`numberOfReservations: ${numberOfReservations}`);

      this.reservations = reservations;
      this.ride = ride;
      this.numberOfReservations = numberOfReservations;

      if (this.ride != null) {
        this.rider = this.ride.driver;
      }

      for (let reservation of this.reservations) {
        if (reservation.trip.id === Number(this.id)) {
          if (reservation.user.id != this.rider?.id) {
            this.riders.push(reservation.user);
          }
        }
      }

      this.numberOfReservations = this.riders.length + 1;
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) this.subscription.unsubscribe();
  }

  getRiderNames(): string {
    return this.riders.map(rider => rider.name).join(', ');
  }

  del(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(`deleting ${this.id}`);
    this.subscription = this.rideService.deleteRide(this.id).subscribe((results) => {
      console.log(`result: ${results}`);
      this.router.navigate(['/trip']);
    });
  }

  isOwnerAndOnlyReservation(): boolean {
    let user = this.authService.getDecodedToken();
    if (user) {
      return this.ride?.driver.id == user.Id && this.numberOfReservations == 1;
    }
    return false;
  }

  joined(): boolean {
    console.log("reservation: " + this.reservations);
    //check if user is part of the ride
    for (let reservation of this.reservations) {
      if (reservation.user.id === Number(this.authService.getDecodedToken().Id)) {
        if (reservation.trip.id === Number(this.id)) {
          return true;
        }
      }
    }
    return false;
  }

  isOwner(): boolean {
    let user = this.authService.getDecodedToken();
    if (user) {
      return this.ride?.driver.id == user.Id;
    }
    return false;
  }

  join(): void {
    let user = this.authService.getDecodedToken();
    if (user && this.ride) {
      console.log(`joining ${this.ride.id}`);
      this.subscription = this.rideService.joinRide(this.ride.id, user.Id).subscribe((results) => {
        console.log(`result: ${results}`);
        this.router.navigate(['/trip']);
      });
    }
  }

  leave(): void {
    let currentUserId: number = this.authService.getDecodedToken().Id;
    let currentTripId: number | undefined = this.ride?.id;

    console.log('UserId:', currentUserId);
    console.log('TripId:', currentTripId);
    console.log('DriverId:', this.rider?.id);

    if (currentUserId && currentTripId) {
      // Zoek de reservering van de gebruiker voor de huidige rit
      this.reservation = this.reservations.find(r => r.trip.id === currentTripId && r.userId == currentUserId) ?? null;
      console.log('Reservation:', this.reservation);

      if (this.reservation) {
        // Als de reservering is gevonden, verlaat dan de rit
        this.subscription = this.rideService.leaveRide(this.reservation.id).subscribe((results) => {
          console.log(`Result: ${results}`);

          if (this.ride?.driver.id == currentUserId) {
            // Zoek een andere reservering dan die van de huidige gebruiker
            let otherReservation = this.reservations.find((r) => r.user.id !== this.rider?.id && r.trip.id === currentTripId);
            console.log('OtherReservation:', otherReservation);
            console.log(otherReservation?.tripId);
            console.log(otherReservation?.userId);

            if (otherReservation && this.ride) {
              // Update de rit met de nieuwe bestuurder
              this.rideService.updateRide(
                String(this.ride.id),
                this.ride.departure.id,
                this.ride.destination.id,
                this.ride.startTime,
                this.ride.endTime,
                otherReservation.user.id,
                this.ride.car.id
              ).subscribe((updateResult) => {
                console.log(`Update ride result: ${updateResult}`);
                // Navigeer naar de trip pagina nadat de rit is bijgewerkt
                this.router.navigate(['/trip']);
              });
            }
          } else {
            // Als de vertrekkende gebruiker geen bestuurder was, navigeer dan gewoon naar de trip pagina
            this.router.navigate(['/trip']);
          }
        });
      }
    }
  }

  isStartTimePassed(): boolean {
    if (!this.ride?.startTime) {
      return true;
    }

    const startTime = new Date(this.ride.startTime);
    const currentTime = new Date();
    const marginMinutes = 60;

    const timeDifferenceMinutes = (startTime.getTime() - currentTime.getTime()) / (1000 * 60);

    return timeDifferenceMinutes <= marginMinutes;
  }

}
