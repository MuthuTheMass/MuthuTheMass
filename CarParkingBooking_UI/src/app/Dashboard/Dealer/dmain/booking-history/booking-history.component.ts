import { Component, signal } from '@angular/core';
import { RecentBookingInDealerDashBoard } from '../../../../Service/Model/UserDetails';
import { BackStoreService } from '../../../../Service/store/back-store.service';
import { DealerDataService } from '../../../../Service/Backend/dealer-data.service';
import { ModalComponent } from "../../../../shared/modal/modal.component";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalService } from '../../../../shared/service/modal.service';
import { CarBookingDetailDto } from '../../../../Service/Model/BookingDealerModal';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-booking-history',
  standalone: true,
  imports: [ModalComponent,CommonModule,DatePipe],
  templateUrl: './booking-history.component.html',
  styleUrl: './booking-history.component.css'
})
export class BookingHistoryComponent {

  bookingData = signal<RecentBookingInDealerDashBoard[]>([] as RecentBookingInDealerDashBoard[]);
  singleBookingDetail = signal<CarBookingDetailDto>({} as CarBookingDetailDto);
  showModal:boolean = false;
  isSingleBookingDetailAvailable:boolean = true;

constructor(private bsStore:BackStoreService,
    private dealerService: DealerDataService,
    private modalService: ModalService
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





  openModal(bookingId:string) {
    this.showModal = true;

    this.dealerService.getSingleBookingDetialByBookingId(bookingId).subscribe({
      next:(result)=>{
        this.singleBookingDetail.set(result);
        this.isSingleBookingDetailAvailable = false;
      },
      error:(err)=>{
        this.isSingleBookingDetailAvailable = true;
        console.error(err);
      }
    });
  }

  closeModal() {
    this.showModal = false;
    this.singleBookingDetail.set({} as CarBookingDetailDto);
  }

    protected readonly print = print;

  printBooking(bookingID: string) {
    this.dealerService.getPdfOfSingleBooking(bookingID).subscribe((blob) => {
      const fileURL = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = fileURL;
      a.download = `ZenPark_${bookingID}.pdf`;
      a.click();
      window.URL.revokeObjectURL(fileURL);
    });
  }
}
