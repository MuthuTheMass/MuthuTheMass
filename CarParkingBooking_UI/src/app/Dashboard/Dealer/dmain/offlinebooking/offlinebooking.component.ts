import { Component, OnChanges, OnInit, signal, Signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DealerDataService } from '../../../../Service/Backend/dealer-data.service';
import { BackStoreService } from '../../../../Service/store/back-store.service';
import { BookingDto, BookingProcessDetails, BookingSources, CarBookingDates, CustomerDetails, Status } from '../../../../Service/Model/BookingDealerModal';
import { ErrorMessageComponent } from "../../../../shared/error-message/error-message.component";
import {ToastsService} from "../../../../custom_components/error_toast/toasts.service";
import {ToastVM} from "../../../../Service/Model/notificationVm";
import {NotificationType} from "../../../../Service/Enums/NotificationType";
import {NgClass} from "@angular/common";

@Component({
  selector: 'app-offlinebooking',
  templateUrl: './offlinebooking.component.html',
  styleUrls: ['./offlinebooking.component.css'],
  standalone:true,
  imports: [ReactiveFormsModule, ErrorMessageComponent,NgClass]
})
export class OfflinebookingComponent implements OnInit {
  bookingForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dealerService: DealerDataService,
    private bsStore:BackStoreService,
    private _toastService:ToastsService,
  ) {}

  ngOnInit(): void {
    this.bookingForm = this.fb.group({
      fullName: ['', Validators.required],
      email: [''],
      mobileNumber: ['', [Validators.required,Validators.pattern(/^\d{10}$/)]],
      address: [''],
      proof: [''],
      proofNumber: [''],
      AllotedSlot: ['', Validators.required],
      vehicleNumber: ['', [
        Validators.required,
        Validators.pattern(/^[A-Z]{2} \d{2} [A-Z]{2} \d{4}$/)
      ]],
      vehicleModel: ['', Validators.required],
      bookingDate: [this.getLocalDateTime()],
      advanceAmount: ['₹', [
        Validators.required,
        Validators.pattern(/^₹? \d+(\.\d{1,2})?$/)
      ]],
    });

    if(this.bsStore.dealerLoggedData().email == undefined){
      var dealerData = JSON.parse(localStorage.getItem("Dealer") ?? {} as any);
      this.bsStore.dealerLoggedData.set(dealerData as any);
    }

    setInterval(() => {
      this.bookingForm.get('bookingDate')?.setValue(this.getLocalDateTime());
    }, 1000);

    console.log('data:',this.bookingForm);
  }

  onSubmit(): void {
    var offlinebooking:BookingDto = {
      dealerEmail: this.bsStore.dealerLoggedData().email,
      customerDetails: {
          customerName: this.bookingForm.get('fullName')?.value,
          email: this.bookingForm.get('email')?.value,
          mobileNumber: this.bookingForm.get('mobileNumber')?.value,
          address: this.bookingForm.get('address')?.value,
          proof: {
            Type: this.bookingForm.get('proof')?.value,
            Number: this.bookingForm.get('proofNumber')?.value
            }
      } as CustomerDetails,
      vehicleInfo:{
        vehicleNumber: this.bookingForm.get('vehicleNumber')?.value,
        vehicleModel: this.bookingForm.get('vehicleModel')?.value,
      },
      bookingSource:BookingSources.Dealer,
      bookingDate: {
        from: this.bookingForm.get('bookingDate')?.value,
        to: undefined,
      } as CarBookingDates,
      advanceAmount: this.bookingForm.get('proofNumber')?.value,
      bookingStatus:{
        state: BookingProcessDetails.VehicleEntered,
      } as Status,
      allottedSlot: this.bookingForm.get('AllotedSlot')?.value,
    } as BookingDto


    if (this.bookingForm.valid) {
      this.dealerService.BookingByOffline(offlinebooking).subscribe({
        next: (result: any) => {
          console.log(result);
          this._toastService.showToast({message:'Successfully Booking initiated', type:NotificationType.Success} as ToastVM);
        },
        error: (err: any) => {
          console.log(err);
          this._toastService.showToast({message:'Failed To Initiate Booking Process, Please try again later.', type:NotificationType.Error} as ToastVM);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }

  private getLocalDateTime(): string {
    const now = new Date();
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }
}
