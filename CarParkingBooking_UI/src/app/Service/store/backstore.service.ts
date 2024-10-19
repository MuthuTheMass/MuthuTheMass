import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { dealerVM } from '../Model/dealermodal';

@Injectable({
  providedIn: 'root'
})
export class BackstoreService {

  public dealerData:BehaviorSubject<dealerVM[]> = new BehaviorSubject<dealerVM[]>([]);



  



  constructor() { }







  
}
