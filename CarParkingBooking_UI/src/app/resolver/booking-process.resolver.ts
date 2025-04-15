import { Resolve, ResolveFn } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { BookingProcessByUser } from '../Service/store/bookingProcessByUser';
import { UserDetailsService } from '../Service/Backend/user-details.service';
import { BookingDto } from '../Service/Model/BookingDealerModal';

@Injectable({ providedIn: 'root' })
export class bookingProcessResolver implements Resolve<BookingDto> {
  constructor(
    private booking: BookingProcessByUser,
    private userService: UserDetailsService,
  ) {}

  resolve(): Observable<BookingDto> {
    if (this.booking.BookingProcesDetails().customerDetails.customerName == null) {
      return of({} as BookingDto);
    }

    return this.userService.GetBookedDetails(
      this.booking.BookingProcesDetails().bookingDate.userBookingDate ?? new Date(),
      this.booking.BookingProcesDetails().customerDetails.email ?? '',
    );
  }
}
