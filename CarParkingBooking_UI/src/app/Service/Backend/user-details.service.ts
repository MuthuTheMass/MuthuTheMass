import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { userDetails } from '../Model/UserDetails';
import { BackstoreService } from '../store/backstore.service';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  constructor(private http:HttpClient,private bstore:BackstoreService) { }


  userFullDetails(userId:string){
    this.http.get<userDetails>(environment.apiUrl+"Users/userfull?userEmail="+userId).subscribe(
      (response:userDetails) => {
        this.bstore.userDetails.next(response);
      },
    );
  }


}
