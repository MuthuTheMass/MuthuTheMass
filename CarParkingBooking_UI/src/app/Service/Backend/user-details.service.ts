import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { userDetails } from '../Model/UserDetails';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  constructor(private http:HttpClient) { }


  userFullDetails(userId:string){
    this.http.get<userDetails>(environment.apiUrl+"Users/userfull?userEmail="+userId).subscribe(
      (response:userDetails) => {

      },
      (error:HttpErrorResponse) => {

      }
    );
  }
}
