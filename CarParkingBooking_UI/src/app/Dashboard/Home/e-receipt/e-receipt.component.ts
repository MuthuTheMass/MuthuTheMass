import { Component } from '@angular/core';

@Component({
  selector: 'app-e-receipt',
  standalone: true,
  imports: [],
  templateUrl: './e-receipt.component.html',
  styleUrl: './e-receipt.component.css'
})
export class EReceiptComponent {


  printInvoice(){
    window.print();
}

const container: HTMLElement | null = document.querySelector(".container");
const qrInput: HTMLInputElement | null = container?.querySelector(".form input") as HTMLInputElement;
const generateBtn: HTMLButtonElement | null = container?.querySelector(".form button") as HTMLButtonElement;
const qrImg: HTMLImageElement | null = container?.querySelector(".qr-code img") as HTMLImageElement;

 preValue: string | undefined;

  generateBtn?.addEventListener("click", () => {
    const qrValue: string = qrInput?.value.trim() || '';
    if (!qrValue || preValue === qrValue) return;
    preValue = qrValue;
    generateBtn.innerText = "Generating QR Code...";
    qrImg.src = `https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=${qrValue}`;

    qrImg.addEventListener("load", () => {
        container?.classList.add("active");
        generateBtn.innerText = "Generate QR Code";
    });
});

qrInput?.addEventListener("keyup", () => {
    if (!qrInput.value.trim()) {
        container?.classList.remove("active");
        preValue = "";
    }
});





}
