import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { VehicleDetialsService } from '../../../Service/Backend/vehicle-detials.service';
import { CameraCaptureComponent } from '../../../shared/camera-capture/camera-capture.component';
import { SlidersComponent } from '../../../shared/sliders/sliders.component';
import { CommonService } from '../../service/common.service';
import { ValueValidatorsComponent } from '../../../shared/value-validators/value-validators.component';
import { isValid } from 'ngx-bootstrap/chronos/create/valid';
import { ErrorMessageComponent } from '../../../shared/error-message/error-message.component';
import { miniVehicleModal, VehicleModal } from '../../../Service/Model/VehicleModal';
import { BackStoreService } from '../../../Service/store/back-store.service';
import { ToastsService } from '../../../custom_components/error_toast/toasts.service';
import { ToastVM } from '../../../Service/Model/notificationVm';
import { NotificationType } from '../../../Service/Enums/NotificationType';

@Component({
  selector: 'app-uservehicle',
  standalone: true,
  imports: [ReactiveFormsModule, CameraCaptureComponent, SlidersComponent, ErrorMessageComponent],
  templateUrl: './user-vehicle.component.html',
  styleUrl: './user-vehicle.component.css',
})
export class UserVehicleComponent implements AfterViewInit, OnInit {
  editVehicleDetails!: any;
  vehicleDetails!: FormGroup;

  constructor(
    private router: Router,
    private activateRoute: ActivatedRoute,
    private backVehicle: VehicleDetialsService,
    protected commonService: CommonService,
    private vehicleDetailsService: VehicleDetialsService,
    private bsStore: BackStoreService,
    private _toastService: ToastsService,
  ) {}

  ngOnInit(): void {
    this.vehicleDetails = new FormGroup({
      VehicleNumber: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[A-Z]{2} \d{2} [A-Z]{2} \d{4}$/),
      ]),
      VehicleNumberImage: new FormControl('', [Validators.required]),
      VehicleImage: new FormControl('', [Validators.required]),
      VehicleName: new FormControl('', [Validators.required]),
      DriverName: new FormControl(),
      DriverNumber: new FormControl(),
      vehicleModal: new FormControl('', [Validators.required]),
      mobileNumber: new FormControl('', [Validators.required, Validators.pattern(/^\d{10}$/)]),
    });

    this.InitialEditVehicleDetails();
    this.bsStore.getUserDetialsByEmailId();
  }

  ngAfterViewInit() {}

  InitialEditVehicleDetails() {
    this.activateRoute.queryParamMap.subscribe((params) => {
      this.editVehicleDetails = {
        emailId: params.get('emailId'),
        vehicleId: params.get('vehicleId'),
      };
    });
  }

  confirmAddVehicle() {
    console.log(this.vehicleDetails);

    const vehicleDetail = {
      vehicleNumber: this.vehicleDetails.get('VehicleNumber')?.value,
      vehicleName: this.vehicleDetails.get('VehicleName')?.value,
      vehicleImage: this.vehicleDetails.get('VehicleImage')?.value,
      vehicleNumberImage: this.vehicleDetails.get('VehicleNumberImage')?.value,
      driverName: this.vehicleDetails.get('OwnerName')?.value,
      driverPhoneNumber: this.vehicleDetails.get('DriverNumber')?.value,
      vehicleModel: this.vehicleDetails.get('vehicleModal')?.value,
      alternative_Phone_Number: this.vehicleDetails.get('mobileNumber')?.value,
    } as VehicleModal;

    console.log(this.vehicleDetails);
    console.log(vehicleDetail);

    if (!this.vehicleDetails.errors) {
      this.vehicleDetailsService
        .addVehicleDetails(vehicleDetail, this.bsStore.userDetails.getValue().email)
        .subscribe({
          next: () => {
            this._toastService.showToast({
              message: 'Successfully Vehicle Added.',
              type: NotificationType.Success,
            } as ToastVM);
            this.router.navigate(['/main/dealer-details']);
          },
          error: (error) => {
            console.error('Error adding vehicle details:', error);
            this._toastService.showToast({
              message: `Error adding vehicle details:${error}`,
              type: NotificationType.Success,
            } as ToastVM);
            alert('Error adding vehicle details');
          },
        });

      // this.router.navigate(['/main/dealer-details']);
    }
  }

  setImage($event: string, formControlName: string) {
    this.vehicleDetails.get(formControlName)?.setValue($event.split(',')[1]);
  }

  cancel() {
    this.commonService.cancel();
  }

  isValid = (formControlName: any) => {
    return this.vehicleDetails.get(formControlName)?.errors;
  };
}
