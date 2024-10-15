import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { userLogin, userSignUp } from '../Model/UserModels';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',

})
export class UserAuthService {

  constructor(private http:HttpClient,private router:Router) { }

  UserLogin(login:userLogin) : any{
    this.http.post("https://localhost:7045/api/Authorization/userlogin",login)
             .subscribe(
              (data)=>{
                this.router.navigate(['/main']);

             },
            error =>{
             alert(error.status as HttpErrorResponse);
            });
  }

  UserSignUp(signUp:userSignUp):any{

    return this.http.post<userSignUp>("https://localhost:7045/api/Authorization/signup",signUp).toPromise();
  }

}
