import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { dealerVM } from '../Model/dealermodal';
import { BackstoreService } from '../store/backstore.service';
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root',
})
export class DealerdatasService {


 constructor(public httpClient:HttpClient, public backStoreService:BackstoreService ){

 }

  getalluserdata(){

    var body = {
      "searchFrom": "string",
      "filters": [
      ]
    }

     this.httpClient.post<any>(environment.apiUrl+"Dealer/search",body).subscribe(data =>{
      console.log(data)
      this.backStoreService.dealerData.next(data as dealerVM[]);
     });
  }

}
