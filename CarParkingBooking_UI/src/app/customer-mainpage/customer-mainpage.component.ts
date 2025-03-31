import { Component } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-customer-mainpage',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './customer-mainpage.component.html',
  styleUrl: './customer-mainpage.component.css'
})
export class CustomerMainpageComponent {

}
