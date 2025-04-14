import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { VehicleDetialsService } from '../../../Service/Backend/vehicle-detials.service';
import { CameraCaptureComponent } from '../../../shared/camera-capture/camera-capture.component';
import { SlidersComponent } from '../../../shared/sliders/sliders.component';

@Component({
  selector: 'app-uservehicle',
  standalone: true,
  imports: [ReactiveFormsModule, CameraCaptureComponent, SlidersComponent],
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
  ) {}

  ngOnInit(): void {
    this.vehicleDetails = new FormGroup({
      VehicleNumber: new FormControl('', [Validators.required]),
      VehicleNumberImage: new FormControl('', [Validators.required]),
      VehicleImage: new FormControl('', [Validators.required]),
      OwnerName: new FormControl('', [Validators.required]),
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

  userconfirmbooking() {
    // this.router.navigate(['/main/dealer-details']);
  }

  setImage($event: string, formControlName: string) {
    this.vehicleDetails.get(formControlName)?.setValue($event);
  }
}
