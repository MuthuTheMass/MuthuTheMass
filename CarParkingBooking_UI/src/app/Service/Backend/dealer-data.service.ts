import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
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

  getalluserdata(){

    let loc;
    let error:any = "";

  //   this.locationService.getCurrentLocation()
  //    .then(position => {
  //      loc = {
  //       Latitude: position.coords.latitude,
  //       Longitude: position.coords.longitude
  //      };
  //      error = null; // Clear any previous errors

  //      const body = {
  //       "searchFrom": "string",
  //       "filters": [],
  //       "userLocation": loc
  //     };
  
  //     this.httpClient.post<any>(environment.apiUrl+"Dealer/search",body).subscribe(data =>{
  //       console.log(data)
  //       this.backStoreService.dealerData.next(data as dealerVM[]);
  //      });
  //    })
  //    .catch(err => {
  //      error = err.message;
  //      loc = null; // Clear previous location
  //    });

  this.locationService.getIPLocation()
     .then(position => {
      //  loc = {
      //   Latitude: position.coords.latitude,
      //   Longitude: position.coords.longitude
      //  };
       error = null; // Clear any previous errors

       const body = {
        "searchFrom": "string",
        "filters": [],
        "userLocation": position
      };
  
      this.httpClient.post<any>(environment.apiUrl+"Dealer/search",body).subscribe(data =>{
        console.log(data)
        this.backStoreService.dealerData.next(data as dealerVM[]);
       });
     })
     .catch(err => {
       error = err.message;
       loc = null; // Clear previous location
     });

  }

  public getUserLocation(): any {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position: GeolocationPosition) => {
          const userLocation = {
            latitude: position.coords.latitude,
            longitude: position.coords.longitude,
          }
          return userLocation;
        });
    }

    return null;
  }

  getNewUsers(dealerEmailId:string):Observable<any>{
   return this.httpClient.get(environment.apiUrl+"Dealer/dealernewusers?userName="+dealerEmailId);
  }
}
