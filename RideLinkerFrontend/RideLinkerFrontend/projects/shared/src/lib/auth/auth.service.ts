import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators"; 
import { User } from "../models/user.interface"
import { Observable, Subject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/account/login';
  private userURL = 'http://localhost:5000/api/user';
  private currentUserSubject: Subject<User | null> = new Subject<User | null>();
  currentUser$: Observable<User | null> = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    console.log('model: ', model);
    return this.http.post(this.apiUrl, model).pipe(
      map((response: any) => {
        const user = response;
        console.log(response);
        if (user && user.token) { 
          localStorage.setItem('token', user.token.result);
          this.loadCurrentUser();
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.apiUrl, model).pipe(
      map((response: any) => {
        const user = response;
        console.log(response);
        if (user && user.token) { 
          localStorage.setItem('token', user.token.result);
        }
      })
    );
  }

  private setToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
  
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null); 
  }

  loadCurrentUser(): void {
    const userId = this.getDecodedToken().Id;
    const token = this.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.userURL}/${userId}`;

    this.http.get<User>(url, { headers }).subscribe(
      (user) => {
        this.currentUserSubject.next(user);
      },
      (error) => {
        console.error('Failed to load current user', error);
      }
    );
  }

  getDecodedToken(): any {
    const token = this.getToken();
    if (!token) return null;
    return JSON.parse(atob(token.split('.')[1]));
  }

  getCurrentUser(): Observable<User> {  
    const userId = this.getDecodedToken().Id;
    const token = this.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.userURL}/${userId}`;

    return this.http.get<User>(url, { headers });
  }
}
