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
      },
      error: (error) => {
        this._toast.showToast({
          message: 'Error fetching booking details',
          type: NotificationType.Error,
        });
      },
    });
  }

  PaymentInitialize(){
    this._userService.PaymentInitialize(this.bookingDetail().dealerEmail).subscribe({
      next: (data) => {
        this.advanceAmount = parseFloat(data);
      }
    });
  }

  // printInvoice() {
  //   window.print();

  //   document.addEventListener('DOMContentLoaded', function () {
  //     // Selectors for user input fields and UI elements
  //     const user_name = document.querySelector('.user-name') as HTMLInputElement;
  //     const user_email = document.querySelector('.user-email') as HTMLInputElement;
  //     const user_phone = document.querySelector('.user-phone') as HTMLInputElement;
  //     const generateCodeButton = document.querySelector('.generate-qr-code') as HTMLButtonElement;
  //     const qrImage = document.querySelector('.qr-image') as HTMLImageElement;
  //     const qrCanvas = document.querySelector('.qr-canvas') as HTMLCanvasElement | null; // Optional, since it's not used yet
  //     const loading = document.querySelector('.loading') as HTMLElement;

  //     generateCodeButton.onclick = async () => {
  //       // Clear previous QR image
  //       qrImage.src = '';

  //       // Get user input values
  //       let name: string = user_name.value.trim();
  //       let email: string = user_email.value.trim();
  //       let phone: string = user_phone.value.trim();

  //       // Prepare user data string
  //       let userData: string = `Name: ${name} Email: ${email} Phone: ${phone}`;
  //       let imgSrc: string = `https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=${userData}`;

  //       // Show loading indicator
  //       loading.style.display = 'block';

  //       // Check if any field is filled in
  //       if (name !== '' || email !== '' || phone !== '') {
  //         try {
  //           // Fetch QR code image
  //           let response = await fetch(imgSrc);
  //           let data = await response.blob();

  //           // Set image source to generated QR code
  //           qrImage.src = URL.createObjectURL(data);
  //           loading.style.display = 'none';

  //           // Revoke the object URL after it's used (good for memory management)
  //           URL.revokeObjectURL(qrImage.src);
  //         } catch (error) {
  //           console.error('Error generating QR code:', error);
  //           loading.style.display = 'none';
  //         }
  //       } else {
  //         alert('Please enter valid field data!!!');
  //         loading.style.display = 'none';
  //       }
  //     };
  //   });
  // }

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
}
