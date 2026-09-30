import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../auth.service';

@Component({
  selector: 'lib-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  subscription: Subscription | undefined = undefined;
  _id: string | null = null;
  Email = '';
  Password = '';
  errorMessage: string | null = null;

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService) {}

  onSubmit(): void {
    console.log('Logging in');
    this.authService.login({ Email: this.Email, Password: this.Password }).subscribe(
      () => {
        console.log('Login successful');
        this.authService.loadCurrentUser();
        this.router.navigate(['/home']);
      },
      (error) => {
        console.error('Login failed', error);
        this.errorMessage = 'Invalide loginpoging. Controleer de gegevens en probeer opnieuw.';
      }
    );
  }
}
