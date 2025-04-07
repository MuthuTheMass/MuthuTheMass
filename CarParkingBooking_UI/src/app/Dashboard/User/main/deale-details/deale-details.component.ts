import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { dealerVM } from '../../../../Service/Model/dealermodal';
import { LoadingComponent } from '../../../../shared/loading/loading.component';
import { UserDetailsService } from '../../../../Service/Backend/user-details.service';
import { VehicleDetailOfSingle } from '../../../../Service/Model/VehicleModal';
import { BackStoreService } from '../../../../Service/store/back-store.service';

@Component({
  selector: 'app-deale-details',
  standalone: true,
  imports: [LoadingComponent],
  templateUrl: './deale-details.component.html',
  styleUrl: './deale-details.component.css',
})
export class DealeDetailsComponent implements OnInit {
  dealerDetail = signal<dealerVM>({} as dealerVM);
  weekDays: { day: string; start: string; stop: string }[] = [];
  vehicleDetails = signal<VehicleDetailOfSingle[]>([]);

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private _bsStore: BackStoreService,
    private _userService: UserDetailsService,
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
  }

  usercardata() {
    this.router.navigate(['/main/uservehicle']);
  }

  usercarolddata() {
    this.router.navigate(['/main/confirmbooking']);
  }

  direction(url: string) {
    window.open(url, '_blank');
  }

  VehicleSelected($event: Event) {
    console.log('vehicleSelected', $event);
  }
}
