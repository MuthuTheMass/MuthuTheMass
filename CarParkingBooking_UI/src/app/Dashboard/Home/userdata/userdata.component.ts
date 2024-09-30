import { Component, ElementRef, ViewChild } from '@angular/core';
import { routes } from '../../../app.routes';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userdata',
  standalone: true,
  imports: [],
  templateUrl: './userdata.component.html',
  styleUrl: './userdata.component.css'
})
export class UserdataComponent {

  
constructor( private router:Router) {
  
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




  @ViewChild('video') videoRef!: ElementRef<HTMLVideoElement>;
  @ViewChild('canvas') canvasRef!: ElementRef<HTMLCanvasElement>;
  @ViewChild('photo') photoRef!: ElementRef<HTMLImageElement>;

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

 
  
  
   Erecepit(){


      this.router.navigate(['/main/erecepit']);
    
  }
  



}
