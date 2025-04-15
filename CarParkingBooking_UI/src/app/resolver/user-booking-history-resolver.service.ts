import { Resolve, ResolveFn } from '@angular/router';
import { Injectable } from '@angular/core';
import { UserDetailsService } from '../Service/Backend/user-details.service';
import { Observable } from 'rxjs';
import { BookingInUserDashBoard } from '../Service/Model/UserDetails';
import { BackStoreService } from '../Service/store/back-store.service';

@Injectable({ providedIn: 'root' })
export class UserBookingHistoryResolver implements Resolve<any> {
  constructor(
    private _userService: UserDetailsService,
    private bsStore: BackStoreService,
  ) {}

  resolve(): Observable<BookingInUserDashBoard[]> {
    this.bsStore.getUserDetialsByEmailId();
    const emailID = this.bsStore.userDetails.getValue().email;
    if (!emailID) {
      // Optional: handle missing ID (redirect or return null/empty)
      return new Observable<BookingInUserDashBoard[]>();
    }

    return this._userService.GetAllBookingForUser(emailID);
  }
}
