import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import {userDetails, UserUpdateData} from '../Model/UserDetails';
import { BackstoreService } from '../store/backstore.service';
import { Observable } from 'rxjs';
import { UserdataComponent } from '../../Dashboard/Home/userdata/userdata.component';
import {observableToBeFn} from "rxjs/internal/testing/TestScheduler";

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  constructor(private http:HttpClient,private bstore:BackstoreService) { }


  userFullDetails(userId:string):Observable<userDetails>{
    return this.http.get<userDetails>(environment.apiUrl+"Users/userfull?userEmail="+userId)
  }

  UpdateData(data:FormData):Observable<any>{
    return this.http.post(environment.apiUrl+"Users/updateuser",data,
      );
  }

}
