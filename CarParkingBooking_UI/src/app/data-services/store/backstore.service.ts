import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { dealerVM } from '../modal/dealermodal.service';

@Injectable({
  providedIn: 'root'
})
export class BackstoreService {

  public dealerData:BehaviorSubject<dealerVM[]> = new BehaviorSubject<dealerVM[]>([]);



  



  constructor() { }







  
}
