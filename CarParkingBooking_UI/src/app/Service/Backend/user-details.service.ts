import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import {userDetails, userDetailsForDealer, UserUpdateData} from '../Model/UserDetails';
import { BackStoreService } from '../store/back-store.service';
import { Observable } from 'rxjs';
import {observableToBeFn} from "rxjs/internal/testing/TestScheduler";

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  constructor(private http:HttpClient,private bstore:BackStoreService) { }


  userFullDetails(userId:string):Observable<userDetails>{
    return this.http.get<userDetails>(environment.apiUrl+"Users/userfull?userEmail="+userId)
  }

  UpdateData(data:FormData):Observable<any>{
    return this.http.post(environment.apiUrl+"Users/updateuser",data,
      );
  }

  GetAllUsers():Observable<userDetailsForDealer[]>{
    return this.http.get<userDetailsForDealer[]>(environment.apiUrl+"Users/getAllUsers");
  }

}
