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

@Component({
  selector: 'app-uservehicle',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CameraCaptureComponent,
    SlidersComponent,
    ValueValidatorsComponent,
    ErrorMessageComponent,
  ],
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
  ) {}

  ngOnInit(): void {
    this.vehicleDetails = new FormGroup({
      VehicleNumber: new FormControl('', [Validators.required]),
      VehicleNumberImage: new FormControl('', [Validators.required]),
      VehicleImage: new FormControl('', [Validators.required]),
      OwnerName: new FormControl('', [Validators.required]),
      DriverName: new FormControl(),
      DriverNumber: new FormControl(),
      vehicleModal: new FormControl('', [Validators.required]),
      mobileNumber: new FormControl('', [Validators.required, Validators.pattern(/^\d{10}$/)]),
    });

    this.InitialEditVehicleDetails();
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
      vehicleImage: this.vehicleDetails.get('VehicleImage')?.value,
      vehicleNumberImage: this.vehicleDetails.get('VehicleNumberImage')?.value,
      driverName: this.vehicleDetails.get('OwnerName')?.value,
      driverPhoneNumber: this.vehicleDetails.get('DriverNumber')?.value,
      vehicleModel: this.vehicleDetails.get('vehicleModal')?.value,
      alternative_Phone_Number: this.vehicleDetails.get('mobileNumber')?.value,
    } as VehicleModal;

    if (!this.vehicleDetails.errors) {
      this.vehicleDetailsService.addVehicleDetails(this.editVehicleDetails).subscribe((res) => {
        if (res.status === 200) {
          alert('Vehicle Details Added Successfully');
          this.router.navigate(['/main/dealer-details']);
        } else {
          alert('Failed to add vehicle details');
        }
      });
    } else {
      alert('Please fill all the required fields');
    }

    // this.router.navigate(['/main/dealer-details']);
  }

  setImage($event: string, formControlName: string) {
    this.vehicleDetails.get(formControlName)?.setValue($event);
  }

  cancel() {
    this.commonService.cancel();
  }

  isValid = (formControlName: any) => {
    return this.vehicleDetails.get(formControlName)?.errors;
  };
}
