import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { dealerVM } from '../../../../Service/Model/dealermodal';
import { UserDetailsService } from '../../../../Service/Backend/user-details.service';
import { VehicleDetailOfSingle } from '../../../../Service/Model/VehicleModal';
import { BackStoreService } from '../../../../Service/store/back-store.service';
import {
  BookingDto,
  BookingProcessDetails,
  BookingSources,
  CarBookingDates,
  CustomerDetails,
  Status,
} from '../../../../Service/Model/BookingDealerModal';
import { FormsModule } from '@angular/forms';
import { ToastsService } from '../../../../custom_components/error_toast/toasts.service';
import { ToastVM } from '../../../../Service/Model/notificationVm';
import { NotificationType } from '../../../../Service/Enums/NotificationType';
import { ErrorMessageComponent } from '../../../../shared/error-message/error-message.component';
import { userDetails } from '../../../../Service/Model/UserDetails';
import { BookingProcessByUser } from '../../../../Service/store/bookingProcessByUser';

@Component({
  selector: 'app-deale-details',
  standalone: true,
  imports: [FormsModule, ErrorMessageComponent],
  templateUrl: './deale-details.component.html',
  styleUrl: './deale-details.component.css',
})
export class DealeDetailsComponent implements OnInit {
  dealerDetail = signal<dealerVM>({} as dealerVM);
  weekDays: { day: string; start: string; stop: string }[] = [];
  vehicleDetails = signal<VehicleDetailOfSingle[]>([]);
  BookingDate: string | null = null;
  Error: {} | null = null;
  selectedVehicleIndex: number | null = null;
  selectedVehicle: VehicleDetailOfSingle | null = null;
  customerDetails = signal<userDetails>({} as userDetails);

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private _bsStore: BackStoreService,
    private _userService: UserDetailsService,
    private _BookingStore: BookingProcessByUser,
    private _toast: ToastsService,
  ) {}

  ngOnInit(): void {
    this.dealerDetail.set(this.route.snapshot.data['dealerId'] || {});

    const timing = this.dealerDetail().dealerTiming;

    this.weekDays = [
      { day: 'Monday', start: timing.monday.start, stop: timing.monday.stop },
      { day: 'Tuesday', start: timing.tuesday.start, stop: timing.tuesday.stop },
      { day: 'Wednesday', start: timing.wednesday.start, stop: timing.wednesday.stop },
      { day: 'Thursday', start: timing.thursday.start, stop: timing.thursday.stop },
      { day: 'Friday', start: timing.friday.start, stop: timing.friday.stop },
      { day: 'Saturday', start: timing.saturday.start, stop: timing.saturday.stop },
      { day: 'Sunday', start: timing.sunday.start, stop: timing.sunday.stop },
    ];

    if (this._bsStore.userDetails.getValue().email == null) {
      var detail = JSON.parse(localStorage.getItem('User') ?? '{}');
      this._bsStore.userDetails.next(detail);
    }

    this._userService
      .GetUserVehicleDetailsForQuickBooking(this._bsStore.userDetails.value.email)
      .subscribe(
        (data: any) => {
          this.vehicleDetails.set(data);
        },
        (error) => {
          console.error('Error fetching vehicle details:', error);
        },
      );

    this._userService.userFullDetails(this._bsStore.userDetails.value.email).subscribe(
      (data: any) => {
        this.customerDetails.set(data);
      },
      (error) => {
        console.error('Error fetching user details:', error);
      },
    );
  }

  usercardata() {
    this.router.navigate(['/main/uservehicle']);
  }

  ConfirmBooking() {
    if (this.selectedVehicleIndex == null) {
      this.Error = { VehicleCheckBox: true };
      return;
    }
    if (this.BookingDate == null) {
      this.Error = { dateTime_Local: true };
      return;
    } else {
      this.Error = null;
    }
    console.log(this.customerDetails());

    var offlinebooking: BookingDto = {
      dealerEmail: this.dealerDetail().dealerEmail,
      customerId: this.customerDetails().email,
      customerDetails: {
        customerName: this.customerDetails().name,
        email: this.customerDetails().email,
        mobileNumber: this.customerDetails().mobileNumber,
        address: this.customerDetails().address,
        proof: {
          Type: '',
          Number: '',
        },
      } as CustomerDetails,
      vehicleInfo: {
        vehicleNumber: this.selectedVehicle?.vehicleNumber,
        vehicleModel: this.selectedVehicle?.vehicleModel ?? '',
        vehicleImage: '',
      },
      bookingSource: BookingSources.User,
      bookingDate: {
        userBookingDate: new Date(this.BookingDate!),
        from: undefined,
        to: undefined,
      } as CarBookingDates,
      advanceAmount: '',
      bookingStatus: {
        state: BookingProcessDetails.InProgress,
      } as Status,
      allottedSlot: '',
    } as BookingDto;
    this._BookingStore.BookingProcesDetails.set(offlinebooking);

    this._userService.ConfirmBooking(offlinebooking).subscribe({
      next: (result: any) => {
        this._toast.showToast({
          message: 'Booking processing',
          type: NotificationType.Success,
        } as ToastVM);
        console.log('Booking confirmed:', result);
        this.router.navigate(['/main/confirmbooking']);
      },
      error: (error) => {
        this._toast.showToast({ message: error.message, type: NotificationType.Error } as ToastVM);
        console.error('Error confirming booking:', error);
      },
    });
  }

  direction(url: string) {
    window.open(url, '_blank');
  }

  onSelectVehicle(index: number, vehicle: VehicleDetailOfSingle): void {
    if (this.selectedVehicleIndex === index) {
      // If already selected, unselect
      this.selectedVehicleIndex = null;
      this.selectedVehicle = null;
    } else {
      this.selectedVehicleIndex = index;
      this.selectedVehicle = vehicle;
    }

    console.log('Selected Vehicle:', this.selectedVehicle);
  }
}
