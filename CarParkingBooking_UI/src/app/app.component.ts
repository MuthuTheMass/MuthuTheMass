import {} from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { ToastComponent } from "./custom_components/error_toast/toast.component";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, ReactiveFormsModule, FormsModule, ToastComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CarParkingBooking';
}


