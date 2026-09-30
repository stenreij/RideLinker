import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Car, Location } from '../../../shared/src/public-api';
import { AuthService } from '../../../shared/src/lib/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private apiUrl = 'http://localhost:5000/api/car';

  location: Location = { id: 101, name: 'Toronto', address: '123 Yonge St' };
  cars: Car[] = [];

  constructor(private http: HttpClient, private authService: AuthService) { }

  getCars(): Observable<Car[]> {
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    return this.http.get<Car[]>(`${this.apiUrl}`, { headers });
  }

  getCar(id: number): Observable<Car> {
    // const url = `${this.apiUrl}/${id}`;
    let token = this.authService.getToken();
    let headers = { 'Authorization': `Bearer ${token}` };
    // return this.http.get<Car>(url);
    return this.http.get<Car>(`${this.apiUrl}/${id}`, { headers });
  }
}
