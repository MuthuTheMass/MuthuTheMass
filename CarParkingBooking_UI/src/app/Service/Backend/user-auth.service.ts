import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DealerSignUp, Login, SignUp } from '../Model/UserModels';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { LoginResponse } from '../Model/BackendUserModels';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',

})
export class UserAuthService {

  constructor(private http:HttpClient,private router:Router) { }

  Login(login:Login) : any{
    this.http.post<LoginResponse>(environment.apiUrl+"Authorization/userlogin", login)
              .subscribe(
              (data:LoginResponse) => {
                    localStorage.clear();
                    localStorage.setItem("User",JSON.stringify(data));
                    this.router.navigate(['/main']);
              });

  }

  SignUp(signUp:SignUp):any{

    return this.http.post<SignUp>(environment.apiUrl+"Authorization/signup",signUp);
  }

  DealerLogin(login:Login) : any{
    this.http.post<LoginResponse>(environment.apiUrl+"Authorization/dealerlogin", login)
              .subscribe(
              (data:LoginResponse) => {
                    localStorage.clear();
                    localStorage.setItem("Dealer",JSON.stringify(data));
                    this.router.navigate(['/dhome']);
              });

  }

  DealerSignUp(signUp:DealerSignUp):any{

    return this.http.post<SignUp>(environment.apiUrl+"Authorization/dealersignup",signUp);
  }

}
