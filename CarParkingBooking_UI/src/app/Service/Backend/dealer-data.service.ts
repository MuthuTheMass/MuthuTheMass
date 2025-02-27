import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { dealerVM } from '../Model/dealermodal';
import {environment} from "../../../environments/environment";
import {BackStoreService} from "../store/back-store.service";
import {LocationService} from "../UIService/location.service";

@Injectable({
  providedIn: 'root',
})
export class DealerDataService {


 constructor(
   public httpClient:HttpClient,
   public backStoreService:BackStoreService,
   public locationService:LocationService){

 }

  getalluserdata(pageNumber:number,pageSize:number):Observable<dealerVM[]>{

       const body = {
        "searchFrom": "string",
        "filters": [],
        "pageNumber":pageNumber,
        "pageSize":pageSize
      };
  
      return this.httpClient.post<any>(`${environment.apiUrl}Dealer/search`, body);

  }
  getNewUsers(dealerEmailId:string):Observable<any>{
   return this.httpClient.get(environment.apiUrl+"Dealer/dealernewusers?userName="+dealerEmailId);
  }
}


