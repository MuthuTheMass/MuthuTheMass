import { Injectable, signal } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import {ToastVM} from "../../Service/Model/notificationVm";


@Injectable({
  providedIn: 'root'
})
export class ToastsService {
  private toastMessage = signal<ToastVM[]>([]); // Signal to hold toast messages

  get toastMessage$() {
    return this.toastMessage; // Getter for the toast message signal
  }

  showToast(toast: ToastVM) {
    this.toastMessage.update(toasts => toasts ? [...toasts, toast] : [toast]); // Ensure toasts array is always initialized
    setTimeout(() => this.clearToast(), 5000); // Clears after 5 seconds
  }

  clearToast() {
    this.toastMessage.set([]); // Clear the toast message
  }
}
