import { Component, ElementRef, ViewChild } from '@angular/core';
import { routes } from '../../../app.routes';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { VehicleDetialsService } from '../../../Service/Backend/vehicle-detials.service';


@Component({
  selector: 'app-userdata',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './userdata.component.html',
  styleUrl: './userdata.component.css'
})
export class UserdataComponent {

  @ViewChild('video') videoRef!: ElementRef<HTMLVideoElement>;
  @ViewChild('canvas') canvasRef!: ElementRef<HTMLCanvasElement>;
  @ViewChild('photo') photoRef!: ElementRef<HTMLImageElement>;
  editVehicleDetails!: any;
  vehicleDetials!: FormGroup;
  
constructor( 
  private router:Router,
  private activateRoute:ActivatedRoute,
  private backVehicle:VehicleDetialsService) {
  
}

  // vechilemodal(){
  //   document.getElementById("myDropdown")!.classList.toggle("show");
  // }
  
  // filterFunction() {
  //   const input: HTMLInputElement | null = document.getElementById("myInput") as HTMLInputElement;
  //   const filter: string = input.value.toUpperCase();
  //   const div: HTMLElement | null = document.getElementById("myDropdown");
  //   const a: HTMLAnchorElement[] = Array.from(div!.getElementsByTagName("a"));
    
  //   for (let i: number = 0; i < a.length; i++) {
  //     const txtValue: string = a[i].textContent || a[i].innerText || "";
  //     if (txtValue.toUpperCase().indexOf(filter) > -1) {
  //       a[i].style.display = "";
  //     } else {
  //       a[i].style.display = "none";
  //     }
  //   }
  // }


 ngOnInit():void{
  this.activateRoute.queryParamMap.subscribe(params => {
    this.editVehicleDetails = { emailid: params.get('emailId'), vehicleId: params.get('vehicleId')}
    this.backVehicle.getVehicleSingleByUserIDAndVehicleNumber(this.editVehicleDetails.emailId,this.editVehicleDetails.vehicleId).subscribe(
      (next)=>{
        console.log(next);
      }
    )
  });
  if(this.editVehicleDetails.emailId!=null){
    
  }

  this.vehicleDetials = new FormGroup({
    VehicleNumber:new FormControl(),
    VehicleNumberImage:new FormControl(),
    OwnerName:new FormControl(),
    DriverNumber: new FormControl(),
    vehicleModal:new FormControl(),
    mobileNumber: new FormControl(),
  });

 }



  ngAfterViewInit() {
    this.setupCamera();
  }

  async setupCamera() {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ video: true });
      this.videoRef.nativeElement.srcObject = stream;
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

 
  
  
  userconfirmbooking(){
      this.router.navigate(['/main/confirmbooking']);
    
  }
  



}
function next(value: Object): void {
  throw new Error('Function not implemented.');
}

