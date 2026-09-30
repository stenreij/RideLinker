import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { isValid } from 'date-fns';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  canActivate(): boolean {
    if (this.authService.isLoggedIn()) {
      const decodedToken = this.authService.getDecodedToken();
      if (decodedToken && decodedToken.exp) {
        const expirationDate = new Date(decodedToken.exp * 1000);
        const currentDate = new Date();
        if (expirationDate > currentDate) {
          return true;
        } else {
          console.error('JWT-token is verlopen.');
          this.authService.logout();
          this.router.navigate(['/login']);
          return false;
        }
      } else {
        console.error('Ongeldig of ontbrekend JWT-token.');
        this.authService.logout();
        this.router.navigate(['/login']);
        return false;
      }
    } else {
      console.error('Gebruiker is niet ingelogd.');
      this.router.navigate(['/login']);
      return false;
    }
  }
}  