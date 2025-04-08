import { Resolve, ResolveFn } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { BookingProcessByUser } from '../app/Service/store/bookingProcessByUser';
import { UserDetailsService } from '../app/Service/Backend/user-details.service';

@Injectable({ providedIn: 'root' })
export class bookingProcessResolver implements Resolve<any> {
  constructor(
    private booking: BookingProcessByUser,
    private userService: UserDetailsService,
  ) {}

  resolve(): Observable<any> {
    if (this.booking.BookingProcesDetails().customerDetails.customerName == null) {
      return of(null);
    }

    return this.userService.GetBookedDetails(
      this.booking.BookingProcesDetails().bookingDate.from,
      this.booking.BookingProcesDetails().customerDetails.email ?? '',
    );
  }
}
