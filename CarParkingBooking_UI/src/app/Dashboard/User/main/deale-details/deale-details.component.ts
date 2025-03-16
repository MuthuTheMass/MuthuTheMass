import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-deale-details',
  standalone: true,
  imports: [],
  templateUrl: './deale-details.component.html',
  styleUrl: './deale-details.component.css'
})
export class DealeDetailsComponent {

  constructor(private router: Router, ) {
  }


  usercardata(){
    this.router.navigate(['/main/uservehicle']);
  }

  usercarolddata(){
    this.router.navigate(['/main/confirmbooking']);
  }
}
