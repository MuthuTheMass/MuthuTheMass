import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { userLogin } from '../Model/UserModels';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',

})
export class UserAuthService {

  constructor(private http:HttpClient) { }

  UserLogin(login:userLogin) : any{
    this.http.post("https://localhost:7045/api/Authorization/userlogin",login)
             .subscribe(data => {
                console.log(data);
             });
  }

}
