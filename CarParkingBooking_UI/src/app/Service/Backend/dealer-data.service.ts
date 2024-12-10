import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { dealerVM } from '../Model/dealermodal';
import {environment} from "../../../environments/environment";
import {BackStoreService} from "../store/back-store.service";

@Injectable({
  providedIn: 'root',
})
export class DealerDataService {


 constructor(public httpClient:HttpClient, public backStoreService:BackStoreService ){

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
