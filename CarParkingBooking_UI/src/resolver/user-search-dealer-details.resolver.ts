import { Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { UserDetailsService } from '../app/Service/Backend/user-details.service';

@Injectable({ providedIn: 'root' })
export class UserSearchDealerDetailsResolver implements Resolve<any> {
  constructor(private _userService: UserDetailsService) {}

  resolve(): Observable<any> {
    const dealerID = sessionStorage.getItem('dealerID');

    if (!dealerID) {
      // Optional: handle missing ID (redirect or return null/empty)
      return of(null);
    }

    return this._userService.GetDealerDetails(dealerID);
  }
}
