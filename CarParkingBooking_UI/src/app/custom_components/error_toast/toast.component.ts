import { Component, inject, OnInit } from '@angular/core';
import { ToastsService } from './toasts.service';
import { NgIf } from '@angular/common';
import {NotificationType} from "../../Service/Enums/NotificationType";

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.css'
})
export class ToastComponent  {
  toastMessage$ = inject(ToastsService).toastMessage$; // Get the toast message signal

  clearToast() {
    inject(ToastsService).clearToast(); // Clear the toast message
  }

  protected readonly NotificationType = NotificationType;
}
