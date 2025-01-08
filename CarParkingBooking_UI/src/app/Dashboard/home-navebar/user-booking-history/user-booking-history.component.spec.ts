import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserBookingHistoryComponent } from './user-booking-history.component';

describe('UserBookingHistoryComponent', () => {
  let component: UserBookingHistoryComponent;
  let fixture: ComponentFixture<UserBookingHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserBookingHistoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserBookingHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
