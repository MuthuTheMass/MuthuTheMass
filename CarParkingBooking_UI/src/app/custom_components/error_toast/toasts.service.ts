import { Injectable, signal } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastsService {
  private toastMessage = signal<string | null>(null); // Signal to hold toast messages

  get toastMessage$() {
    return this.toastMessage; // Getter for the toast message signal
  }

  showToast(message: string) {
    this.toastMessage.set(message); // Set the toast message
    setTimeout(() => this.clearToast(), 5000); // Clears after 5 seconds
  }

  clearToast() {
    this.toastMessage.set(null); // Clear the toast message
  }
}
