import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { userDetails, UserDetailsForDealer, UserUpdateData } from '../Model/UserDetails';
import { BackStoreService } from '../store/back-store.service';
import { Observable } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { dealerVM } from '../Model/dealermodal';
import { BookingDto } from '../Model/BookingDealerModal';

@Injectable({
  providedIn: 'root',
})
export class UserDetailsService {
  constructor(
    private http: HttpClient,
    private bstore: BackStoreService,
  ) {}

  userFullDetails(userId: string): Observable<userDetails> {
    return this.http.get<userDetails>(environment.apiUrl + 'Users/userfull?userEmail=' + userId);
  }

  UpdateData(data: FormData): Observable<any> {
    return this.http.post(environment.apiUrl + 'Users/updateuser', data);
  }

  GetDealerDetails(dealerId: string): Observable<dealerVM> {
    return this.http.get<dealerVM>(
      environment.apiUrl + 'Users/FullDealerDetails?dealerId=' + dealerId,
    );
  }

  GetUserVehicleDetailsForQuickBooking(emailId: string) {
    return this.http.get(environment.apiUrl + 'Users/VehicleDetailsForBooking?emailId=' + emailId);
  }

  ConfirmBooking(booking: BookingDto): Observable<BookingDto> {
    return this.http.post<BookingDto>(environment.apiUrl + 'BookingUserSlot/UserBooking', booking);
  }

  GetBookedDetails(BookedDate: Date, customerEmail: string): Observable<BookingDto> {
    return this.http.get<BookingDto>(
      environment.apiUrl +
        'BookingUserSlot/UserBooking?dateTime=' +
        new Date(BookedDate).toISOString() +
        '&customerEmail=' +
        customerEmail,
    );
  }
}
