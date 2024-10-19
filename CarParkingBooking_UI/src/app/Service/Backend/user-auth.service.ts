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
    this.http.post(environment.apiUrl+"Authorization/userlogin", login)
              .subscribe({
              next: (data) => {
                    this.router.navigate(['/main']);
              },
              error: (error: HttpErrorResponse) => {
                    alert(`Error: ${error.status} - ${error.message}`);
              }
  });

  }

  SignUp(signUp:SignUp):any{

    return this.http.post<SignUp>(environment.apiUrl+"Authorization/signup",signUp);
  }

  DealerLogin(login:Login) : any{
    this.http.post(environment.apiUrl+"Authorization/dealerlogin", login)
              .subscribe({
              next: (data) => {
                    this.router.navigate(['/main']);
              },
              error: (error: HttpErrorResponse) => {
                    alert(`Error: ${error.status} - ${error.message}`);
              }
  });

  }

  DealerSignUp(signUp:DealerSignUp):any{

    return this.http.post<SignUp>(environment.apiUrl+"Authorization/dealersignup",signUp);
  }

}
