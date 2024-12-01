import { Component } from '@angular/core';
import { ArticalComponent } from "./artical/artical.component";
import { RouterOutlet } from '@angular/router';
import {FormControl, FormGroup, FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DealerdatasService } from '../../../Service/Backend/dealerdatas.service';
import { BackstoreService } from '../../../Service/store/backstore.service';
import {RatingModule} from "ngx-bootstrap/rating";



@Component({
  selector: 'app-main',
  standalone: true,
  imports: [ArticalComponent, RouterOutlet, RatingModule, FormsModule, CommonModule,],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

  dealerdetails: DealerdatasService | any;
  rating: number = 4.5;
  currentRate: number = 2;


  constructor(public ms: DealerdatasService, protected bstore: BackstoreService) {


    this.dealerdetails = new FormGroup({
      dealername: new FormControl(),
      email: new FormControl(),
      parkingaddress: new FormControl(),
      starrating: new FormControl()

    });
  }


  getalldealerdetails() {
    this.ms.getalluserdata();
  }


  convertStringToFloat(value: string) {
    return parseFloat(value);
  }


  ngOnInit() {
    this.getalldealerdetails();
  }

  getdata() {
    const searchbox = document.getElementById('loac') as HTMLInputElement;
    const putdata = document.getElementById('sdata');
    const sbtn = document.getElementById('btnd');

    if (putdata) {
      putdata.innerHTML = searchbox?.value;
    }
  }

  ariaValueText(current: number, max: number) {
    return `${current} out of ${max} hearts`;
  }

  data: number = 3;
  hoveringOver: any;
}
