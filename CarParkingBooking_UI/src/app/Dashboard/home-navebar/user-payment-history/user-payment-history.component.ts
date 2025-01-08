import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-user-payment-history',
  standalone: true,
  imports: [],
  templateUrl: './user-payment-history.component.html',
  styleUrl: './user-payment-history.component.css'
})
export class UserPaymentHistoryComponent {
  constructor(private router:Router) {

  }

  viewDetails(){
    this.router.navigate(['/main/erecepit']);
  }
}
