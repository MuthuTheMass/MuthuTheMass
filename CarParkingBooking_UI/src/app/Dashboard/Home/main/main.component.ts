import { Component } from '@angular/core';
import { ArticalComponent } from "./artical/artical.component";
import { RouterOutlet } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { BackstoreService } from '../../../data-services/store/backstore.service';
import { DealerdatasService } from '../../../data-services/service/dealerdatas.service';


@Component({
  selector: 'app-main',
  standalone: true,
  imports: [ArticalComponent,RouterOutlet],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

dealerdetails:DealerdatasService|any;


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
