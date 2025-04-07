import { Component, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { BookingDto, CarBookingDetailDto } from '../../../../Service/Model/BookingDealerModal';

@Component({
  selector: 'app-user-confirm-booking',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './user-confirm-booking.component.html',
  styleUrl: './user-confirm-booking.component.css',
})
export class UserConfirmBookingComponent {
  bookingDetails = signal({} as BookingDto);

  constructor(
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  bookingslot() {
    this.bookingDetails.set(this.route.snapshot.data['booking']);
    console.log('data', this.bookingDetails);
    this.router.navigate(['/main/customerhistory']);
  }
}
