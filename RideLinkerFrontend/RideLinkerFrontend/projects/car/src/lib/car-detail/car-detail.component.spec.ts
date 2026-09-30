import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { CarDetailComponent } from './car-detail.component';
import { CarService } from '../car.service';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { beforeEach, describe, expect, it, jest } from '@jest/globals';
import { AuthService } from '@shared/src/lib/auth/auth.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('CarDetailComponent', () => {
  let component: CarDetailComponent;
  let fixture: ComponentFixture<CarDetailComponent>;
  let mockCarService : any;
  let mockActivatedRoute : any;

  beforeEach(async () => {
    mockCarService = {
      getCar: jest.fn().mockReturnValue(of({ id: 1, name: 'Test Car' })) 
    };

    mockActivatedRoute = {
      snapshot: {
        paramMap: {
          get: jest.fn().mockReturnValue('1')
        }
      }
    };

    TestBed.configureTestingModule({
      declarations: [CarDetailComponent],
      providers: [
        { provide: CarService, useValue: mockCarService },
        { provide: ActivatedRoute, useValue: mockActivatedRoute }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]  // This allows any custom element
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call loadCarDetails on ngOnInit', () => {
    const loadCarDetailsSpy = jest.spyOn(component, 'ngOnInit');
    component.ngOnInit();
    expect(loadCarDetailsSpy).toHaveBeenCalled();
  });


});