import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BackstoreService } from '../store/backstore.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleDetialsService {

  constructor(private http:HttpClient,private bstore:BackstoreService) { }

  vehicleDetailsByUserID(userID:string){
    this.http.get(environment.apiUrl+"Vehicle?UserId="+userID).subscribe(
      (response:any)=>{
        this.bstore.VehicleDetails.next(response);
      }
    );
  }
}
