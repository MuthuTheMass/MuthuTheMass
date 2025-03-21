import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserConfirmBookingComponent } from './user-confirm-booking.component';

describe('UserConfirmBookingComponent', () => {
  let component: UserConfirmBookingComponent;
  let fixture: ComponentFixture<UserConfirmBookingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserConfirmBookingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserConfirmBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
