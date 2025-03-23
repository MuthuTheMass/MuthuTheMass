import { Component } from '@angular/core';
import { RazorpaybuttonComponent } from './razorpaybutton/razorpaybutton.component';

@Component({
  selector: 'app-e-receipt',
  standalone: true,
  imports: [RazorpaybuttonComponent],
  templateUrl: './e-receipt.component.html',
  styleUrl: './e-receipt.component.css'
})
export class EReceiptComponent {


  printInvoice(){
    window.print();


// const container: HTMLElement | null = document.querySelector(".container");
// const qrInput: HTMLInputElement | null = container?.querySelector(".form input") as HTMLInputElement;
// const generateBtn: HTMLButtonElement | null = container?.querySelector(".form button") as HTMLButtonElement;
// const qrImg: HTMLImageElement | null = container?.querySelector(".qr-code img") as HTMLImageElement;

//  preValue: string | undefined;

//   generateBtn?.addEventListener("click", () => {
//     const qrValue: string = qrInput?.value.trim() || '';
//     if (!qrValue || preValue === qrValue) return;
//     preValue = qrValue;
//     generateBtn.innerText = "Generating QR Code...";
//     qrImg.src = `https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=${qrValue}`;

//     qrImg.addEventListener("load", () => {
//         container?.classList.add("active");
//         generateBtn.innerText = "Generate QR Code";
//     });
// });

// qrInput?.addEventListener("keyup", () => {
//     if (!qrInput.value.trim()) {
//         container?.classList.remove("active");
//         preValue = "";
//     }
// });




document.addEventListener("DOMContentLoaded", function() {
  // Selectors for user input fields and UI elements
  const user_name = document.querySelector('.user-name') as HTMLInputElement;
  const user_email = document.querySelector('.user-email') as HTMLInputElement;
  const user_phone = document.querySelector('.user-phone') as HTMLInputElement;
  const generateCodeButton = document.querySelector('.generate-qr-code') as HTMLButtonElement;
  const qrImage = document.querySelector('.qr-image') as HTMLImageElement;
  const qrCanvas = document.querySelector('.qr-canvas') as HTMLCanvasElement | null; // Optional, since it's not used yet
  const loading = document.querySelector('.loading') as HTMLElement;

  generateCodeButton.onclick = async () => {
      // Clear previous QR image
      qrImage.src = '';

      // Get user input values
      let name: string = user_name.value.trim();
      let email: string = user_email.value.trim();
      let phone: string = user_phone.value.trim();

      // Prepare user data string
      let userData: string = `Name: ${name} Email: ${email} Phone: ${phone}`;
      let imgSrc: string = `https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=${userData}`;

      // Show loading indicator
      loading.style.display = 'block';

      // Check if any field is filled in
      if (name !== '' || email !== '' || phone !== '') {
          try {
              // Fetch QR code image
              let response = await fetch(imgSrc);
              let data = await response.blob();

              // Set image source to generated QR code
              qrImage.src = URL.createObjectURL(data);
              loading.style.display = 'none';

              // Revoke the object URL after it's used (good for memory management)
              URL.revokeObjectURL(qrImage.src);
          } catch (error) {
              console.error('Error generating QR code:', error);
              loading.style.display = 'none';
          }
      } else {
          alert('Please enter valid field data!!!');
          loading.style.display = 'none';
      }
  };
});



  }

}
