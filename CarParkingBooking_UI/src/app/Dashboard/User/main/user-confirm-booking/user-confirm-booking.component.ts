import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { BookingDto, CarBookingDetailDto } from '../../../../Service/Model/BookingDealerModal';
import { ValueValidatorsComponent } from "../../../../shared/value-validators/value-validators.component";

@Component({
  selector: 'app-user-confirm-booking',
  standalone: true,
  imports: [RouterLink, ValueValidatorsComponent],
  templateUrl: './user-confirm-booking.component.html',
  styleUrl: './user-confirm-booking.component.css',
})
export class UserConfirmBookingComponent implements OnInit {
  bookingDetails = signal({} as BookingDto);

  constructor(
    private router: Router,
    private route: ActivatedRoute,
  ) {}

ngOnInit(): void {
 this. initialize();
}

    initialize():void{
      this.bookingDetails.set(this.route.snapshot.data['booking']);
      console.log('booking confirmed details',this.bookingDetails());
    }

  bookingslot() {
    this.router.navigate(['/main/customerhistory']);
  }
}
