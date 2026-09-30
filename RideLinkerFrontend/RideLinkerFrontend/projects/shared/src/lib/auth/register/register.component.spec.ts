import { ComponentFixture, TestBed } from '@angular/core/testing';
import { describe, beforeEach, it, expect, jest } from '@jest/globals'
import { RegisterComponent } from './register.component';
import { ActivatedRoute } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegisterComponent],
      imports: [HttpClientModule, FormsModule],
      providers: [
        { provide: ActivatedRoute, useValue: {} }
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
