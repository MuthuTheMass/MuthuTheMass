import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserdataService {
  uname!:string;
  password!:string;
  email!:string;
  mobile!:number;
  gender!:string;
  country!:string;
  
}
