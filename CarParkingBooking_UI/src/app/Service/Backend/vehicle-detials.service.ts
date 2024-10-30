import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BackstoreService } from '../store/backstore.service';
import { environment } from '../../../environments/environment';
import {VehicleModal} from "../Model/VehicleModal";

@Injectable({
  providedIn: 'root'
})
export class VehicleDetialsService {

  constructor(private http:HttpClient,private bstore:BackstoreService) { }

  vehicleDetailsByUserID(userID:string){
    this.http.get(environment.apiUrl+"Vehicle?UserId="+userID).subscribe(
      (response:any)=>{
        this.bstore.VehicleData.next(response);
      }
    );
  }

  halfVehicleDetailsByUserID(userID:string){
    this.http.get<VehicleModal>(environment.apiUrl+"Vehicle/halfvehicledetails?UserId="+userID).subscribe(
      (response:VehicleModal)=>{
        this.bstore.VehicleDetail.next(response);
      }
    );
  }
}
