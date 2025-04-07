import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-user-confirm-booking',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './user-confirm-booking.component.html',
  styleUrl: './user-confirm-booking.component.css'
})
export class UserConfirmBookingComponent {

  constructor(private router:Router) {
  }
  bookingslot(){

      this.router.navigate(['/main/customerhistory']);
  }
}
