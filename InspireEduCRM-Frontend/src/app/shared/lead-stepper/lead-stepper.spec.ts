import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeadStepper } from './lead-stepper';

describe('LeadStepper', () => {
  let component: LeadStepper;
  let fixture: ComponentFixture<LeadStepper>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeadStepper]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeadStepper);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
