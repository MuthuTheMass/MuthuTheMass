import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { dealerVM } from '../Model/dealermodal';
import { BackstoreService } from '../store/backstore.service';

@Injectable({
  providedIn: 'root',
})
export class DealerdatasService {


  dealerapi = "https://localhost:7045/api/";


 constructor(public mc:HttpClient,public bstore:BackstoreService ){

 }





  getalluserdata(){

    var body = {
      "searchFrom": "string",
      "filters": [
      ]
    }

     this.mc.post<any>(this.dealerapi+"Dealer/search",body).subscribe(data =>{
      console.log(data)
      this.bstore.dealerData.next(data as dealerVM[]);
     });
  }

}
