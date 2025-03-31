import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DealerSignUp, Login, SignUp } from '../Model/UserModels';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { LoginResponse } from '../Model/BackendUserModels';
import { environment } from '../../../environments/environment';
import {BackStoreService} from "../store/back-store.service";

@Injectable({
  providedIn: 'root',

})
export class UserAuthService {

  constructor(private http:HttpClient,
              ) { }

  Login(login:Login) : Observable<LoginResponse>{
    return this.http.post<LoginResponse>(environment.apiUrl+"Authorization/userlogin", login);

  }

  SignUp(signUp:SignUp):any{
    return this.http.post<SignUp>(environment.apiUrl+"Authorization/signup",signUp);
  }

  DealerLogin(login:Login) : Observable<LoginResponse>{
    return this.http.post<LoginResponse>(environment.apiUrl+"Authorization/dealerlogin", login);
  }

  DealerSignUp(signUp:DealerSignUp):any{

    return this.http.post<SignUp>(environment.apiUrl+"Authorization/dealersignup",signUp);
  }

}
