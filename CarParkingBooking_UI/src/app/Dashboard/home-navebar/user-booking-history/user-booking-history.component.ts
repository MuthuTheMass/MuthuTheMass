import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-user-booking-history',
  standalone: true,
  imports: [],
  templateUrl: './user-booking-history.component.html',
  styleUrl: './user-booking-history.component.css'
})
export class UserBookingHistoryComponent {
constructor(private router:Router) {

}

  viewDetails(){
    this.router.navigate(['/main/erecepit']);
  }
}
