import { Component, EventEmitter, Output, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularCropperjsModule } from 'angular-cropperjs';
import Cropper from 'cropperjs';

@Component({
  selector: 'app-image-uploader',
  standalone: true,
  imports: [CommonModule, AngularCropperjsModule],
  templateUrl: './image-uploader.component.html',
  styleUrls: ['./image-uploader.component.css'],
})
export class ImageUploaderComponent {
  @Output() imageConfirmed = new EventEmitter<File>();
  @Output() canceled = new EventEmitter<void>();

  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;
  @ViewChild('angularCropper') angularCropper: any;

  imageUrl: string | null = null;
  croppedImage: string | null = null;
  showCropper = false;
  showPreview = false;

  // Correct cropper configuration property name
  cropperOptions: Cropper.Options = {
    aspectRatio: 1,
    viewMode: 1,
    autoCrop: true,
    movable: true,
    zoomable: true,
    scalable: true,
    responsive: true,
    checkCrossOrigin: false,
    background: false,
    guides: true,
    center: true,
    highlight: false,
    cropBoxMovable: true,
    cropBoxResizable: true,
  };

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const reader = new FileReader();

      reader.onload = (e: ProgressEvent<FileReader>) => {
        this.imageUrl = e.target?.result as string;
        this.showCropper = true;
        this.showPreview = false;
      };

      reader.readAsDataURL(file);
    }
  }

  confirmCrop(): void {
    if (!this.angularCropper?.cropper) return;

    const croppedCanvas = this.angularCropper.cropper.getCroppedCanvas();
    if (!croppedCanvas) return;

    this.croppedImage = croppedCanvas.toDataURL('image/jpeg', 0.92);
    const file = this.dataURLtoFile(this.croppedImage ?? '', 'cropped-image.jpg');

    this.imageConfirmed.emit(file);
    this.showPreview = true;
    this.showCropper = false;
  }

  cancelCrop(): void {
    this.resetComponent();
    this.canceled.emit();
  }

  resetComponent(): void {
    this.imageUrl = null;
    this.croppedImage = null;
    this.showCropper = false;
    this.showPreview = false;

    if (this.fileInput?.nativeElement) {
      this.fileInput.nativeElement.value = '';
    }
  }

  private dataURLtoFile(dataurl: string, filename: string): File {
    const arr = dataurl.split(',');
    const mime = arr[0].match(/:(.*?);/)![1];
    const bstr = atob(arr[1]);
    let n = bstr.length;
    const u8arr = new Uint8Array(n);

    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime });
  }
}
