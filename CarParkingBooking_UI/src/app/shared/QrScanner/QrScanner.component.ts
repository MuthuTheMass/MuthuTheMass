import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {BrowserQRCodeReader, IScannerControls} from "@zxing/browser";
import {DatePipe} from "@angular/common";
import {LoadingComponent} from "../loading/loading.component";

@Component({
  selector: 'app-QrScanner',
  templateUrl: './QrScanner.component.html',
  standalone: true,
  imports: [
    DatePipe,
    LoadingComponent
  ],
  styleUrls: ['./QrScanner.component.css']
})
export class QrScannerComponent implements OnInit, OnDestroy {
  private codeReader: BrowserQRCodeReader | null = null;
  private scannerControls: IScannerControls | null = null;
  scannedData: string | null = null;
  error: string | null = null;
  isScanning = false;
  isLoading = false; // Added loading state
  @Input() bookingDetails: boolean = false;
  @Output() scannedQrCode:EventEmitter<string> = new EventEmitter<string>();

  async ngOnInit() {
    await this.initScanner();
  }

  ngOnDestroy() {
    this.stopScanner();
  }

  private async initScanner() {
    try {
      this.isLoading = true; // Start loading
      this.codeReader = new BrowserQRCodeReader();
      this.isScanning = true;

      const videoInputDevices = await BrowserQRCodeReader.listVideoInputDevices();

      if (videoInputDevices.length === 0) {
        this.error = 'No camera found';
        this.isLoading = false;
        return;
      }

      this.scannerControls = await this.codeReader.decodeFromVideoDevice(
        videoInputDevices[0].deviceId,
        'videoElement',
        (result, error) => {
          if (result) {
            this.handleScan(result.getText());
          }
          if (error) {
            // console.error('Scan error:', error);
            this.isLoading = false;
          }
        }
      );

      this.isLoading = false; // Stop loading after initialization
    } catch (err) {
      this.error = 'Failed to initialize scanner';
      this.isLoading = false;
      console.error('Scanner error:', err);
    }
  }

  private handleScan(data: string) {
    this.isLoading = true; // Start loading when processing scan
    this.scannedData = data;
    this.stopScanner();

    // Simulate API call (replace with your actual API call)
    setTimeout(() => {
      this.bookingDetails = true;
      this.isLoading = false; // Stop loading after data is loaded
      this.scannedQrCode.emit(data);
    }, 1000);
  }

  private stopScanner() {
    if (this.scannerControls) {
      this.scannerControls.stop();
      this.scannerControls = null;
    }
    this.isScanning = false;
  }

  restartScanner() {
    this.scannedData = null;
    this.bookingDetails = false;
    this.error = null;
    this.initScanner();
  }

  StopBooking(id: string) {
    this.stopScanner();
    this.scannedData = null;
    this.bookingDetails = false;
    this.error = null;
    this.initScanner();
  }
}
