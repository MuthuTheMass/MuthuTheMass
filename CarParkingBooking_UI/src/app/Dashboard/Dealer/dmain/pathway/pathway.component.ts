import {Component, signal, WritableSignal} from '@angular/core';
import {QrScannerComponent} from "../../../../shared/QrScanner/QrScanner.component";
import {DealerDataService} from "../../../../Service/Backend/dealer-data.service";
import {CarBookingDetailDto} from "../../../../Service/Model/BookingDealerModal";
import {DatePipe} from "@angular/common";
import { DealerOfflinePaymentComponent } from "./DealerOfflinePayment/DealerOfflinePayment.component";

@Component({
  selector: 'app-pathway',
  standalone: true,
  imports: [
    QrScannerComponent,
    DatePipe,
    DealerOfflinePaymentComponent
],
  templateUrl: './pathway.component.html',
  styleUrl: './pathway.component.css'
})
export class PathwayComponent {

  bookingDetail:WritableSignal<CarBookingDetailDto> = signal<CarBookingDetailDto>({} as CarBookingDetailDto);
  constructor(private DealerData: DealerDataService) {
  }

  ngOnInit() {

  }

  GetDetails($event: string) {
    const encodedId = encodeURIComponent($event);
    this.DealerData.getBookingDetialsByQrCode(encodedId).subscribe({
      next: (result: any) => {
        this.bookingDetail.set(result);
        console.log(result);
      },
      error: (err: any) => {
        console.log(err);
      }
    });
  }

  restartScanner() {
    this.bookingDetail.set({} as CarBookingDetailDto);
  }

  StopBooking(bookingId: any) {

  }
}
