import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { dealerVM, offlinebookingVM } from '../Model/dealermodal';
import {environment} from "../../../environments/environment";
import {BackStoreService} from "../store/back-store.service";
import {LocationService} from "../UIService/location.service";
import { CarBookingDetailDto } from '../Model/BookingDealerModal';

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
   return this.httpClient.get(environment.apiUrl+"Dealer/dealernewusers?emailId="+dealerEmailId);
  }

  BookingByOffline(data:offlinebookingVM):Observable<any>{
    data.dealerEmailId = this.backStoreService.dealerLoggedData().email;
    return this.httpClient.post(environment.apiUrl+"Dealer/OfflineBooking",data);
  }

  getRecentBooking(email: string):Observable<any> {
    return this.httpClient.get(environment.apiUrl+"Dealer/DealerBookings?emailId="+email);
  }

  getSingleBookingDetialByBookingId(id:string):Observable<CarBookingDetailDto>{
    return this.httpClient.get<CarBookingDetailDto>(environment.apiUrl+"BookingUserSlot/GetSingleBookingDetailByBookingId?bookingId="+id);
  }

  getBookingDetialsByQrCode(encryptedQrCode:string):Observable<CarBookingDetailDto>{
    return this.httpClient.get<CarBookingDetailDto>(environment.apiUrl+"BookingUserSlot/GetBookingDetailByEncryptedId?EncryptedId="+encryptedQrCode);
  }

}


