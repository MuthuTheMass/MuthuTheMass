import { Component } from '@angular/core';
import { ArticalComponent } from "./artical/artical.component";
import { RouterOutlet } from '@angular/router';
import {FormControl, FormGroup, FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import {RatingModule} from "ngx-bootstrap/rating";
import {DealerDataService} from "../../../Service/Backend/dealer-data.service";
import {BackStoreService} from "../../../Service/store/back-store.service";



@Component({
  selector: 'app-main',
  standalone: true,
  imports: [RatingModule, FormsModule, CommonModule,],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {


  rating: number = 4.5;
  currentRate: number = 2;



  constructor(public dealerDataService: DealerDataService, protected backStoreService: BackStoreService) {

  }

  ngOnInit():void {
    this.dealerDataService.getalluserdata();
  }

  convertStringToFloat(value: string) {
    return parseFloat(value);
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
