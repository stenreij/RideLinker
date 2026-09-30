import { TestBed, ComponentFixture } from '@angular/core/testing';
import { RideEditComponent } from './ride-edit.component';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { RideService } from '../ride.service';
import { AuthService } from '../../../../shared/src/lib/auth/auth.service';
import { ActivatedRoute, convertToParamMap } from '@angular/router';
import { of } from 'rxjs';
import { describe, beforeEach, it, expect, jest } from '@jest/globals';
import { fail } from 'assert';
import { By } from '@angular/platform-browser';

describe('RideEditComponent', () => {
  let component: RideEditComponent;
  let fixture: ComponentFixture<RideEditComponent>;
  let rideServiceMock: any;

  beforeEach(async () => {
    rideServiceMock = {
      getLocations: jest.fn().mockReturnValue(of([])),
      getCars: jest.fn().mockReturnValue(of([])),
      getRide: jest.fn().mockReturnValue(of({}))
    };

    await TestBed.configureTestingModule({
      declarations: [RideEditComponent],
      imports: [FormsModule, RouterTestingModule, HttpClientModule],
      providers: [
        { provide: RideService, useValue: rideServiceMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(RideEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should disable the save button if required fields are empty', () => {
    // Set ride to have empty values for required fields
    const field = fixture.debugElement.query(By.css('#role')).nativeElement;
    field.value = '';
    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('#saveButton')).nativeElement;
    expect(button.disabled).toBeTruthy();
  });

  it('should enable the save button if required fields are filled', () => {
    // Set ride with valid data for required fields
    component.ride = {
      id: 1,
      departure: { id: 1, name: 'Departure', address: 'Departure Address' },
      destination: { id: 2, name: 'Destination', address: 'Destination Address' },
      startTime: new Date(),
      endTime: new Date(),
      driver: { id: 1, name: 'Driver', email: 'driver@example.com', birthdate: new Date(), role: 1 },
      car: {
        id: 1,
        brand: 'Brand',
        model: 'Model',
        location: { id: 1, name: 'Location', address: 'Location Address' },
        image: 'car.jpg',
        available: true
      }
    };

    fixture.detectChanges();

    // Check if the save button is enabled
    const button = fixture.debugElement.query(By.css('#saveButton')).nativeElement;
    expect(button.disabled).toBeFalsy();
});



});
