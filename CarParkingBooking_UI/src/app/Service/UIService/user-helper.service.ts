import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserHelperService {

  constructor() { }

  base64ToFile(base64String: string, fileName: string): File {
    const byteString = atob(base64String);
    const byteNumbers = new Array(byteString.length);

    for (let i = 0; i < byteString.length; i++) {
      byteNumbers[i] = byteString.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'image/png' }); // Adjust MIME type if needed

    return new File([blob], fileName, { type: 'image/png' });
  }
}
