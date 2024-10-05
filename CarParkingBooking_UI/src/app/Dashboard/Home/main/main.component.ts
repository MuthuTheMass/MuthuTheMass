import { Component } from '@angular/core';
import { ArticalComponent } from "./artical/artical.component";
import { RouterOutlet } from '@angular/router';
import { UserdataService } from '../../../src/service/userdata.service';
import { FormControl, FormGroup } from '@angular/forms';
import { DealerdatasService } from '../../../src/service/dealerdatas.service';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [ArticalComponent,RouterOutlet],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

dealerdetails:DealerdatasService|any;


  constructor( public ms:DealerdatasService) {
    

    
  this.dealerdetails= new FormGroup({
    dealername:new FormControl(),
    email:new FormControl(),
    parkingaddress:new FormControl(),
    starrating:new FormControl()
 
 
 });
  }



  getalldealersdetails(){
  this.ms.getalldealerdata();
  }


  getalldealerdetails(){
    this.ms.getalluserdata().subscribe(r=>{
      console.log(r);
      this.dealerdetails.patchValue({
        dealername:r.uname,
        parkingaddress:r.parkingaddress,
        email:r.email,
        starrating:r.starrating,
  
      })
    },error=>{
      console.log(JSON.stringify(error));
    });
  }












  ngOnInit(){
    
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
