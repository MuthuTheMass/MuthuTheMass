import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { RazorpaybuttonComponent } from './razorpaybutton/razorpaybutton.component';
import { UserDetailsService } from '../../../Service/Backend/user-details.service';
import { ActivatedRoute } from '@angular/router';
import { ToastsService } from '../../../custom_components/error_toast/toasts.service';
import { NotificationType } from '../../../Service/Enums/NotificationType';
import { ValueValidatorsComponent } from "../../../shared/value-validators/value-validators.component";
import { PreUserBookingDetails } from '../../../Service/Model/EreciptUserModal';
import { BackStoreService } from '../../../Service/store/back-store.service';
import { LoadingComponent } from "../../../shared/loading/loading.component";
import { UserPayment } from '../../../Service/Model/Payment';
import { CustomerDetails } from '../../../Service/Model/BookingDealerModal';

@Component({
  selector: 'app-e-receipt',
  standalone: true,
  imports: [RazorpaybuttonComponent, ValueValidatorsComponent, LoadingComponent, LoadingComponent],
  templateUrl: './e-receipt.component.html',
  styleUrl: './e-receipt.component.css',
})
export class EReceiptComponent implements OnInit {

  BookingId: string = '';
  advanceAmount: number = 0;
  isloading: boolean = true;
  bookingDetail = signal({} as PreUserBookingDetails);
  customerdata = signal({} as CustomerDetails);

  constructor(
    private _userService: UserDetailsService,
    private route: ActivatedRoute,
    private _toast: ToastsService,
  ) {}

  ngOnInit() {
    this.initialize();
  }

  initialize() {
    this.route.paramMap.subscribe((params) => {
      this.BookingId = params.get('id')!;
      console.log(this.BookingId);
    });
    this._userService.FetchPreDownloadDetails(this.BookingId).subscribe({
      next: (data) => {
        this.bookingDetail.set(data);
        this.isloading = false;
        this.PaymentInitialize()
        this.setCustomerData();
      },
      error: (error) => {
        this._toast.showToast({
          message: 'Error fetching booking details',
          type: NotificationType.Error,
        });
      },
    });
  }

  setCustomerData() {
    if(this.bookingDetail().customerName == null || this.bookingDetail().customerEmailId == null || this.bookingDetail().customerMobileNumber == null){
      return;
    }
    const data = {
      customerName: this.bookingDetail().customerName,
      email: this.bookingDetail().customerEmailId,
      mobileNumber: this.bookingDetail().customerMobileNumber,
    }as CustomerDetails;

    this.customerdata.set(data);
  }

  PaymentInitialize(){
    this._userService.PaymentInitialize(this.bookingDetail().dealerEmail).subscribe({
      next: (data) => {
        this.advanceAmount = parseFloat(data);
      }
    });
  }

  handlePaymentResult(event: any) {

    const payment = {
      paymentId: event.razorpay_payment_id.razorpay_payment_id,
      bookingId: this.bookingDetail().bookingId,
      customerEmail: this.bookingDetail().customerEmailId,
      currencyMode: null,
      paymentStatus: null,
      paymentMethod: null,
      amount: this.advanceAmount.toLocaleString(),
      createdDate: new Date(),
      updatedDate: new Date(),
      isDeleted: false,
    } as UserPayment;

    if (event.status === 'success') {
      this._userService.PaymentForAdvanceBooking(payment).subscribe({
        next:(data)=>{
          this._toast.showToast({message:"Advance Payment success.",type:NotificationType.Success});
          window.location.reload();
        }
      })
    } else {
      this._toast.showToast({message:"Payment Not processed.",type:NotificationType.Error});
    }
  }

  printInvoice() {
    this._userService.DownloadBookedData(this.BookingId).subscribe({
      next: (data) => {
        const url = window.URL.createObjectURL(data);
        const a = document.createElement('a');
        a.href = url;
        a.download = `Booking_${this.BookingId}.pdf`;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        window.URL.revokeObjectURL(url);
      },
      error: (error) => {
        this._toast.showToast({
          message: 'Error downloading invoice',
          type: NotificationType.Error,
        });
      },
    });
  }
}
