import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { describe, beforeEach, it, expect, jest } from '@jest/globals'
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: AuthService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [FormsModule], // Import FormsModule here
      providers: [
        { provide: AuthService, useValue: {
            login: jest.fn().mockReturnValue(of({ token: 'mock-token' })),
            loadCurrentUser: jest.fn()
        } },
        { provide: Router, useValue: { navigate: jest.fn() } },
        { provide: ActivatedRoute, useValue: {} }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should login succes', () => {

    component.Email = 'test@example.com';
    component.Password = 'password';

    component.onSubmit();

    expect(authService.login).toHaveBeenCalledWith({ Email: 'test@example.com', Password: 'password' });

    expect(router.navigate).toHaveBeenCalledWith(['/home']);
  });
});
