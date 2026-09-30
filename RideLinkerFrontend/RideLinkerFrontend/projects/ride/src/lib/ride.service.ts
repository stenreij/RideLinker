import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CarService } from '../../../car/src/public-api';
import { AuthService } from '../../../shared/src/lib/auth/auth.service';
import { Trip, Location, Car, User, Reservation } from '../../../shared/src/public-api';
import { Observable, catchError, map, throwError, switchMap, forkJoin, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class RideService {

  private apiUrl = 'http://localhost:5000/api/';

  constructor(private http: HttpClient, private authService: AuthService, private carService: CarService,) { }

  getLocations(): Observable<Location[]> {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.get<Location[]>(`${this.apiUrl}location`, { headers });
  }

  getLocation(id: number): Observable<Location> {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.get<Location>(`${this.apiUrl}location/${id}`, { headers });
  }

  getCars(): Observable<Car[]> {
    return this.carService.getCars();
  }

  getRides(): Observable<Trip[]> {
    let token = this.authService.getToken();

    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.get<Trip[]>(`${this.apiUrl}trip`, { headers });
  }

  getAllRides(): Observable<Trip[]> {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.get<Trip[]>(`${this.apiUrl}trip`, { headers });
  }

  getRide(id: string | null): Observable<Trip> {
    let token = this.authService.getToken();
    console.log("getRide");
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.get<Trip>(`${this.apiUrl}trip/${id}`, { headers });
  }

  getRideById(id: number): Observable<Trip[]> {
    let token = this.authService.getToken();
    console.log("getRide");
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.get<Trip[]>(`${this.apiUrl}trip/${id}`, { headers });
  }

  getCurrentUser(): Observable<User | undefined> {
    const token = this.authService.getToken();
    const headers = { 'Authorization': `Bearer ${token}` };

    return this.http.get<User[]>(`${this.apiUrl}user`, { headers }).pipe(
      map(users => {
        const currentUser = users.find(user => user.email === this.authService.getDecodedToken().email);
        return currentUser;
      })
    );
  }

  getUserRides(userId: number): Observable<Trip[]> {

    return this.getReservations().pipe(
      switchMap((reservations: Reservation[]) => {
        console.log('Reservations:', reservations);

        let userReservations: Reservation[] = reservations.filter(reservation =>
          reservation.user.id === userId
        );

        console.log('User reservations:', userReservations);

        if (userReservations.length === 0) {
          console.log('No reservations found for user');
          return of([]); 
        }

        const rideObservables = userReservations.map(reservation =>
          this.getRideById(reservation.trip.id)
        );

        console.log('Ride observables:', rideObservables);

        return forkJoin(rideObservables).pipe(
          map((tripsArrays: Trip[][]) => tripsArrays.flat())
        );
      })
    );
  }

  isUserPartOfRide(userId: number, rideId: number): Observable<boolean> {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };

    return this.http.get<Reservation[]>(`${this.apiUrl}reservation`, { headers }).pipe(
      map((reservations: Reservation[]) => {
        const isPartOfRide = reservations.some(reservation =>
          reservation.user.id === userId && reservation.trip.id === rideId
        );
        return isPartOfRide;
      }),
      catchError(error => {
        console.error('Error in isUserPartOfRide:', error);
        return throwError(() => new Error('Error checking user part of ride'));
      })
    );
  }

  createRide(departureId: number | undefined, destinationId: number | undefined, startTime: Date | null, endTime: Date | null, carId: number | undefined): Observable<Trip> {
    if (!startTime || !endTime || startTime >= endTime) {
      return throwError('Invalid ride timing');
    }
  
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    let driverId: number = this.authService.getDecodedToken().Id;
  
    console.log(departureId);
    console.log(destinationId);
    console.log(startTime);
    console.log(endTime);
    console.log(driverId);
    console.log(carId);

    let trip: any = this.http.post<Trip>(`${this.apiUrl}trip`, {
      departureId, destinationId, startTime, endTime, driverId, carId
    }, { headers })

    this.joinRide(trip.id, driverId)

    return trip;
  }

  updateRide(id: string, departureId: number | undefined, destinationId: number | undefined, startTime: Date | undefined, endTime: Date | undefined, driverId: number | undefined, carId: number | undefined): Observable<Trip> {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };

    return this.http.put<Trip>(`${this.apiUrl}trip/${id}`, {
      departureId, destinationId, startTime, endTime, driverId, carId
    }, { headers })
  }

  deleteRide(id: string | null) {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.delete(`${this.apiUrl}trip/${id}`, { headers })
  }

  public handleError(error: HttpErrorResponse): Observable<any> {
    console.log('handleError in UserService', error);

    return throwError(() => new Error(error.message));
  }

  getReservations(): Observable<Reservation[]> {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.get<Reservation[]>(`${this.apiUrl}reservation`, { headers });
  }

  getNumberOfReservationsForRide(rideId: number): Observable<number> {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    console.log(`getting number of reservations for ride ${rideId}`);
    return this.http.get<Reservation[]>(`${this.apiUrl}reservation`, { headers }).pipe(
      map((reservations) => {
        let numberOfReservations = 0;
        reservations.forEach((reservation) => {
          if (reservation.trip.id == rideId) {
            numberOfReservations++;
          }
        });
        return numberOfReservations;
      })
    );
  }


  joinRide(TripId: number, UserId: number) {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    console.log(`aan het inschrijven`);
    return this.http.post<Reservation>(`${this.apiUrl}reservation`, {
      UserId, TripId
    }, { headers })
  }

  leaveRide(id: number) {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.delete(`${this.apiUrl}reservation/${id}`, { headers })
  }
}
