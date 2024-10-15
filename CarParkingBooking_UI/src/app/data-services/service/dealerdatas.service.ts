import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BackstoreService } from '../store/backstore.service';
import { dealerVM } from '../modal/dealermodal.service';

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
      console.log(data.result)
      this.bstore.dealerData.next(data.result as dealerVM[]);
     });
  }

}
