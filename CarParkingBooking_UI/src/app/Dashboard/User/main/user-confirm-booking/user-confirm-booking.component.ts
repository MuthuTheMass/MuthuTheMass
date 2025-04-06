import { Component } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { routes } from '../../../../app.routes';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-confirm-booking',
  standalone: true,
  imports: [],
  templateUrl: './user-confirm-booking.component.html',
  styleUrl: './user-confirm-booking.component.css',
})
export class UserConfirmBookingComponent {
  constructor(private router: Router) {}

  bookingslot() {
    this.router.navigate(['/main/customerhistory']);
  }
}
