import { Component } from '@angular/core';
import { ArticalComponent } from "./artical/artical.component";
import { RouterOutlet } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DealerdatasService } from '../../../Service/Backend/dealerdatas.service';
import { BackstoreService } from '../../../Service/store/backstore.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgbRating } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-main',
  standalone: true,
  imports: [ArticalComponent,RouterOutlet,CommonModule,NgbRating],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

dealerdetails:DealerdatasService|any;
sample:number = 3;


  constructor( public ms:DealerdatasService , protected bstore:BackstoreService) {
    

    
  this.dealerdetails= new FormGroup({
    dealername:new FormControl(),
    email:new FormControl(),
    parkingaddress:new FormControl(),
    starrating:new FormControl()
 
 
 });
  }


  getalldealerdetails(){
    this.ms.getalluserdata();
  }


  convertStringToFloat(value:string){
      return parseFloat(value);
  }









  ngOnInit(){
    this.getalldealerdetails();
  }

  getdata(){
    const searchbox= document.getElementById('loac') as HTMLInputElement;
    const putdata = document.getElementById('sdata');
    const sbtn = document.getElementById('btnd');
    
    
      if (putdata) {
        putdata.innerHTML = searchbox?.value;
      }
  }
}
