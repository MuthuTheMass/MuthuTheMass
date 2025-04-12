import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { VehicleDetialsService } from '../../../Service/Backend/vehicle-detials.service';
import { CameraCaptureComponent } from '../../../shared/camera-capture/camera-capture.component';

@Component({
  selector: 'app-userdata',
  standalone: true,
  imports: [ReactiveFormsModule, CameraCaptureComponent],
  templateUrl: './user-vehicle.component.html',
  styleUrl: './user-vehicle.component.css',
})
export class UserVehicleComponent implements AfterViewInit, OnInit {
  @ViewChild('video') videoRef!: ElementRef<HTMLVideoElement>;
  @ViewChild('canvas') canvasRef!: ElementRef<HTMLCanvasElement>;
  @ViewChild('photo') photoRef!: ElementRef<HTMLImageElement>;
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

  ngAfterViewInit() {
    this.setupCamera().then((r) => {
      console.log('Camera setup complete');
    });
  }

  InitialEditVehicleDetails() {
    this.activateRoute.queryParamMap.subscribe((params) => {
      this.editVehicleDetails = {
        emailId: params.get('emailId'),
        vehicleId: params.get('vehicleId'),
      };
    });
  }

  async setupCamera() {
    try {
      this.videoRef.nativeElement.srcObject = await navigator.mediaDevices.getUserMedia({
        video: true,
      });
    } catch (err) {
      console.error('Error accessing webcam: ', err);
      alert('Could not access the webcam. Please check your settings.');
    }
  }

  capturePhoto() {
    const canvas = this.canvasRef.nativeElement;
    const context = canvas.getContext('2d');
    if (context) {
      context.drawImage(this.videoRef.nativeElement, 0, 0, canvas.width, canvas.height);
      const dataURL = canvas.toDataURL('image/png');
      this.photoRef.nativeElement.src = dataURL;
    } else {
      console.error('Failed to get canvas context');
    }
  }

  userconfirmbooking() {
    this.router.navigate(['/main/dealer-details']);
  }

  onImageCaptured($event: string) {
    console.log('captured image', $event);
  }
}
