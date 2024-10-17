import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoggedIn } from '../Model/UserBackendModels';
import { Login, SignUp } from '../Model/UserModels';
import { environment } from '../../../environments/environment';

@Injectable(
  providedIn: 'root'
})
export class UserAuthService {
  constructor(private http:HttpClient,private router:Router) { }

  UserLogin(login:Login){
    this.http.post<LoggedIn>(environment.backendURL+"Authorization/userlogin",login)
             .subscribe({
              next:(data:LoggedIn)=>{
                localStorage.setItem('usertoken',data.accessToken)
                this.router.navigate(['/main']);

              },
              error: (error: any) => {
                console.error('Login error:', error);
              }
              // complete: () => {
              //   console.log('Login request complete.');
              // }
            });
  }

  UserSignUp(signUp:SignUp):any{

    return this.http.post<SignUp>(environment.backendURL +"Authorization/signup",signUp);
  }

  DealerLogin(login:Login){
    this.http.post<LoggedIn>(environment.backendURL+"Authorization/dealerlogin",login)
             .subscribe({
              next:(data:LoggedIn)=>{
                localStorage.setItem('dealertoken',data.accessToken);
                this.router.navigate(['/main']);
              
            },
            error: (error:any) => {
              console.error('Login error:', error);
            }
            });
  }

  DealerSignUp(signUp:SignUp){
    return this.http.post<SignUp>(environment.backendURL+"Authorization/dealersignup",signUp);
  }
}
