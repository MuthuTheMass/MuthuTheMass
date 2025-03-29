import { Component, signal } from '@angular/core';
import { RecentBookingInDealerDashBoard } from '../../../../Service/Model/UserDetails';
import { BackStoreService } from '../../../../Service/store/back-store.service';
import { DealerDataService } from '../../../../Service/Backend/dealer-data.service';
import { ModalComponent } from "../../../../shared/modal/modal.component";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-booking-history',
  standalone: true,
  imports: [ModalComponent],
  templateUrl: './booking-history.component.html',
  styleUrl: './booking-history.component.css'
})
export class BookingHistoryComponent {

  bookingData = signal<RecentBookingInDealerDashBoard[]>([] as RecentBookingInDealerDashBoard[]);


constructor(private bsStore:BackStoreService,
    private dealerService: DealerDataService,
    private modalService: NgbModal
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

ModalOpen(){
  const modalRef = this.modalService.open(ModalComponent);
    modalRef.componentInstance.title = 'My Custom Modal';

    // Listen to close if you need
    modalRef.closed.subscribe(() => {
      console.log('Modal closed');
    });
    modalRef.dismissed.subscribe(() => {
      console.log('Modal dismissed');
    });
}
  
}
