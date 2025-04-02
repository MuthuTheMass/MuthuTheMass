import { Component, OnChanges, OnInit, signal, Signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DealerDataService } from '../../../../Service/Backend/dealer-data.service';
import { BackStoreService } from '../../../../Service/store/back-store.service';
import { BookingDto, BookingProcessDetails, BookingSources, CarBookingDates, CustomerDetails, Status } from '../../../../Service/Model/BookingDealerModal';
import { ErrorMessageComponent } from "../../../../shared/error-message/error-message.component";

@Component({
  selector: 'app-offlinebooking',
  templateUrl: './offlinebooking.component.html',
  styleUrls: ['./offlinebooking.component.css'],
  standalone:true,
  imports: [ReactiveFormsModule, ErrorMessageComponent]
})
export class OfflinebookingComponent implements OnInit, OnChanges {
  bookingForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dealerService: DealerDataService,
    private bsStore:BackStoreService,
  ) {}

  ngOnInit(): void {
    this.bookingForm = this.fb.group({
      fullName: ['', Validators.required], // Required
      email: [''], // Optional
      mobileNumber: ['', [Validators.required, Validators.pattern(/^₹?\d+(\.\d{1,2})?$/)]], // Required
      address: [''], // Optional
      proof: [''], // Optional
      proofNumber: [''], // Optional
      AllotedSlot: ['', Validators.required], // Required
      vehicleNumber: ['', Validators.required], // Required
      vehicleModel: ['', Validators.required], // Required
      bookingDate: [this.getLocalDateTime()], // Optional
      advanceAmount: ['₹', Validators.required], // Required
    });

    if(this.bsStore.dealerLoggedData().email == undefined){
      var dealerData = JSON.parse(localStorage.getItem("Dealer") ?? {} as any);
      this.bsStore.dealerLoggedData.set(dealerData as any);
    }

    setInterval(() => {
      this.bookingForm.get('bookingDate')?.setValue(this.getLocalDateTime());
    }, 1000);
  }

  ngOnChanges(): void {
    console.log(this.bookingForm);
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
        state: BookingProcessDetails.InProgress,
      } as Status,
      allottedSlot: this.bookingForm.get('AllotedSlot')?.value,
    } as BookingDto


    if (this.bookingForm.valid) {
      this.dealerService.BookingByOffline(offlinebooking).subscribe({
        next: (result: any) => {
          console.log(result);
        },
        error: (err: any) => {
          console.log(err);
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
