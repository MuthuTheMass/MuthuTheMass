import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserDetailsService } from '../../../../Service/Backend/user-details.service';
import { BackStoreService } from '../../../../Service/store/back-store.service';
import { ToastsService } from '../../../../custom_components/error_toast/toasts.service';
import { ToastVM } from '../../../../Service/Model/notificationVm';
import { NotificationType } from '../../../../Service/Enums/NotificationType';
import { BookingInUserDashBoard } from '../../../../Service/Model/UserDetails';
import { ValueValidatorsComponent } from '../../../../shared/value-validators/value-validators.component';
import { BookingProcessDetails } from '../../../../Service/Model/BookingDealerModal';

@Component({
  selector: 'app-user-booking-history',
  standalone: true,
  imports: [ValueValidatorsComponent],
  templateUrl: './user-booking-history.component.html',
  styleUrl: './user-booking-history.component.css',
})
export class UserBookingHistoryComponent implements OnInit {
  BookingDetailsOfUser: WritableSignal<BookingInUserDashBoard[]> = signal<BookingInUserDashBoard[]>(
    [] as BookingInUserDashBoard[],
  );
  protected readonly BookingProcessDetails = BookingProcessDetails;

  constructor(
    private router: Router,
    private _userService: UserDetailsService,
    private bsStore: BackStoreService,
    private _toast: ToastsService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.bsStore.getUserDetialsByEmailId();
    this.BookingDetailsOfUser.set(this.route.snapshot.data['Bookings']);
  }

  viewDetails(bookingId: string) {
    this.router.navigate(['/main/erecepit', bookingId]);
  }
}
