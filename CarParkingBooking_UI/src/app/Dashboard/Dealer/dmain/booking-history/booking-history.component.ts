import { Component, signal } from '@angular/core';
import { RecentBookingInDealerDashBoard } from '../../../../Service/Model/UserDetails';
import { BackStoreService } from '../../../../Service/store/back-store.service';
import { DealerDataService } from '../../../../Service/Backend/dealer-data.service';

@Component({
  selector: 'app-booking-history',
  standalone: true,
  imports: [],
  templateUrl: './booking-history.component.html',
  styleUrl: './booking-history.component.css'
})
export class BookingHistoryComponent {

  bookingData = signal<RecentBookingInDealerDashBoard[]>([] as RecentBookingInDealerDashBoard[]);


constructor(private bsStore:BackStoreService,
    private dealerService: DealerDataService,
) {

  this.initDetails();
}

initDetails(){
  if(this.bsStore.dealerLoggedData().email == undefined){
    var dealerData = JSON.parse(localStorage.getItem("Dealer") ?? {} as any);
    this.bsStore.dealerLoggedData.set(dealerData as any);
  }
  if(this.bsStore.dealerLoggedData() != null){
  this.dealerService.getRecentBooking(this.bsStore.dealerLoggedData().email).subscribe({
      next:(result)=>{
        this.bookingData.set(result);
      },
      error:(err)=>{
        console.error(err);
      }
  });
  }
}
  
}
