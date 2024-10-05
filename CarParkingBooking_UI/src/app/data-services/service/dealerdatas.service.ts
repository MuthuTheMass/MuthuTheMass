import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DealerdatasService {


  dealerapi = "https://localhost:7045/api/Dealer/search";


 constructor(public mc:HttpClient){

 }

  dealername!:string;
  dpasswoard!:string;
  dmobilenumber!:number;
  parkingaddress!:string;
  starrating!:string;


  public dealeralldata = new BehaviorSubject<string>("");


  getalldealerdata(){
    this.dealeralldata.next("");
  }

  getalluserdata():Observable<any>{
    return this.mc.get(this.dealerapi+"getalldetails");
  }

}
