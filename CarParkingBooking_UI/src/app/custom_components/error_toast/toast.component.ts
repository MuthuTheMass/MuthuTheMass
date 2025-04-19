import { Component, inject, OnInit } from '@angular/core';
import { ToastsService } from './toasts.service';
import { NgClass,  } from '@angular/common';
import {NotificationType} from "../../Service/Enums/NotificationType";

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [NgClass],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.css'
})
export class ToastComponent  {
  toastMessage$ = inject(ToastsService).toastMessage$; // Get the toast message signal
  notifyType: Record<NotificationType, ()=> string>={
    [NotificationType.Success] :()=> "success",
    [NotificationType.Warning] :()=> "warning",
    [NotificationType.Info] :()=> "info",
    [NotificationType.Error] :()=> "error",
    [NotificationType.Custom] :()=> ""
  };
  clearToast() {
    inject(ToastsService).clearToast(); // Clear the toast message
  }

  protected readonly NotificationType = NotificationType;

  notificationStyle=(type:NotificationType)=>{
    const handler = this.notifyType[type];
      return handler();
      
  }
}
