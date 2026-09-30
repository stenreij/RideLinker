import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { CarListComponent } from './car-list.component';
import { CarService } from '../car.service';
import { beforeEach, describe, expect, it, jest } from '@jest/globals';

describe('CarListComponent', () => {
  let component: CarListComponent;
  let fixture: ComponentFixture<CarListComponent>;
  let mockCarService: any;

  beforeEach(async () => {
    mockCarService = {
      getCars: jest.fn().mockReturnValue(of([]))
    };

    await TestBed.configureTestingModule({
      declarations: [CarListComponent],
      providers: [
        { provide: CarService, useValue: mockCarService }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getCars on ngOnInit', () => {
    expect(mockCarService.getCars).toHaveBeenCalled();
  });

 
});