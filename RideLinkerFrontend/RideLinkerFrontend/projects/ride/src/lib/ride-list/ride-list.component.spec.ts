import { TestBed, ComponentFixture } from '@angular/core/testing';
import { RideListComponent } from './ride-list.component';
import { RideService } from '../ride.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { describe, beforeEach, jest, it, expect } from '@jest/globals';
import { AuthService } from '../../../../shared/src/lib/auth/auth.service';
import { get } from 'http';
import { FormsModule } from '@angular/forms';

describe('RideListComponent', () => {
  let component: RideListComponent;
  let fixture: ComponentFixture<RideListComponent>;
  let rideServiceMock: any;
  let authServiceMock: any;

  beforeEach(async () => {
    // Setup mocks
    rideServiceMock = {
      getUserRides: jest.fn().mockReturnValue(of([
        {
          id: 1,
          departure: { id: 1, name: 'Location A', address: 'Address A' },
          destination: { id: 2, name: 'Location B', address: 'Address B' },
          startTime: new Date(),
          endTime: new Date(),
          driver: {
            id: 1,
            name: 'Driver 1',
            birthdate: new Date(),
            email: 'driver@example.com',
            role: 1
          },
          car: {
            id: 1,
            brand: 'Car Brand',
            model: 'Car Model',
            image: 'image-url',
            location: { id: 1, name: 'Location A', address: 'Address A' },
            available: true
          }
        }
      ])),
      getLocations: jest.fn().mockReturnValue(of([])) // Add this line to mock getLocations
    };

    authServiceMock = {
      getDecodedToken: jest.fn().mockReturnValue({ Id: '1' }), // Existing mock
      getToken: jest.fn().mockReturnValue('some_token') // Add this line to mock getToken
    };

    await TestBed.configureTestingModule({
      declarations: [RideListComponent],
      imports: [
        RouterTestingModule, // Needed for any routerLink in the templates
        HttpClientModule, // If your service relies on HttpClient
        FormsModule
      ],
      providers: [
        { provide: RideService, useValue: rideServiceMock },
        { provide: AuthService, useValue: authServiceMock } // Provide the mock
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA] // To avoid errors on unknown elements
    }).compileComponents();

    fixture = TestBed.createComponent(RideListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should update component trips when loadUserRides is called', () => {
    component.loadUserRides();
    fixture.detectChanges(); // Apply changes to the template

    expect(component.trips.length).toBe(1);
    expect(component.trips[0].driver.name).toEqual('Driver 1');
  });
});