import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DealerDataService } from '../../../../Service/Backend/dealer-data.service';
import { BackStoreService } from '../../../../Service/store/back-store.service';

@Component({
  selector: 'app-offlinebooking',
  templateUrl: './offlinebooking.component.html',
  styleUrls: ['./offlinebooking.component.css'],
  standalone:true,
  imports:[ReactiveFormsModule]
})
export class OfflinebookingComponent implements OnInit {
  bookingForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dealerService: DealerDataService,
    private bsStore:BackStoreService,
  ) {}

  ngOnInit(): void {
    this.bookingForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      mobileNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      address: ['', Validators.required],
      proof: ['', Validators.required],
      proofNumber: ['', Validators.required],
      authorityOfIssue: ['', Validators.required],
      vehicleNumber: ['', Validators.required],
      vehicleModel: ['', Validators.required],
      bookingDate: ['', Validators.required]
    });

    if(this.bsStore.dealerLoggedData().email == undefined){
      var dealerData = JSON.parse(localStorage.getItem("Dealer") ?? {} as any);
      this.bsStore.dealerLoggedData.set(dealerData as any);
    }
  }

  onSubmit(): void {
    if (this.bookingForm.valid) {
      this.dealerService.BookingByOffline(this.bookingForm.value).subscribe({
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
}
