import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchoolDetail } from './school-detail';

describe('SchoolDetail', () => {
  let component: SchoolDetail;
  let fixture: ComponentFixture<SchoolDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SchoolDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SchoolDetail);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
