import { Component } from '@angular/core';
import { ImageUploaderComponent } from '../../../../../shared/image-uploader/image-uploader.component';

@Component({
  selector: 'app-dealer-basic-info',
  standalone: true,
  imports: [ImageUploaderComponent],
  templateUrl: './dealer-basic-info.component.html',
  styleUrl: './dealer-basic-info.component.css',
})
export class DealerBasicInfoComponent {
  finalImage: File | null = null;
  finalImageUrl: string | null = null;

  handleImageConfirm(file: File) {
    this.finalImage = file;
    this.finalImageUrl = URL.createObjectURL(file);
  }

  handleCancel() {
    console.log('User canceled cropping');
  }

  uploadImage() {
    if (!this.finalImage) return;
    // Implement your upload logic here
    console.log('Uploading:', this.finalImage);
  }
}
