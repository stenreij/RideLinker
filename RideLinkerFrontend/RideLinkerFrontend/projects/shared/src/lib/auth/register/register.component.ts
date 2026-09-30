import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Import CommonModule
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../auth.service';

@Component({
  selector: 'lib-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  subscription: Subscription | undefined = undefined;
  _id: string | null = null;
  Email = '';
  Password = '';
  RepeatedPassword = '';
  errorMessage: string | null = null;

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService) {}

  onSubmit(): void {
    console.log('Registering');
    if (this.Password !== this.RepeatedPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }
    

  }
}
