import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from '../loading/loading.component';

@Component({
  selector: 'app-camera-capture',
  imports: [CommonModule, LoadingComponent],
  templateUrl: './camera-capture.component.html',
  styleUrl: './camera-capture.component.css',
})
export class CameraCaptureComponent {
  @ViewChild('videoElement', { static: false }) videoRef!: ElementRef<HTMLVideoElement>;
  @Input() existingImage?: string; // base64 string from backend
  @Output() imageCaptured = new EventEmitter<string>();
  isLoadingCamera = false;

  showCamera = false;
  stream: MediaStream | null = null;

  openCamera() {
    this.showCamera = true;
    this.isLoadingCamera = true;
    setTimeout(() => this.setupCamera(), 0);
  }

  async setupCamera() {
    try {
      this.stream = await navigator.mediaDevices.getUserMedia({ video: { facingMode: 'user' } });
      if (this.videoRef?.nativeElement) {
        this.videoRef.nativeElement.srcObject = this.stream;
        this.videoRef.nativeElement.play();
      }
    } catch (err) {
      console.error('Webcam error:', err);
      // alert(
      //   'Could not access the camera. Please allow camera access or check your device settings.',
      // );
      this.showCamera = false;
    } finally {
      this.isLoadingCamera = false;
    }
  }

  capture() {
    if (!this.videoRef?.nativeElement) return;

    const video = this.videoRef.nativeElement;
    const canvas = document.createElement('canvas');
    canvas.width = video.videoWidth || 640;
    canvas.height = video.videoHeight || 480;

    const context = canvas.getContext('2d');
    if (context) {
      context.drawImage(video, 0, 0, canvas.width, canvas.height);
      const imageDataUrl = canvas.toDataURL('image/jpeg');
      this.imageCaptured.emit(imageDataUrl);
      this.existingImage = imageDataUrl;
    }

    this.closeCamera();
  }

  closeCamera() {
    this.showCamera = false;
    if (this.stream) {
      this.stream.getTracks().forEach((track) => track.stop());
      this.stream = null;
    }
  }

  ngOnDestroy(): void {
    this.closeCamera();
  }

  changePicture() {
    this.openCamera();
  }
}
