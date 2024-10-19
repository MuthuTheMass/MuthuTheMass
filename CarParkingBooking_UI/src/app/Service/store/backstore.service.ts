import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { dealerVM } from '../Model/dealermodal';
import { isNumber } from '@ng-bootstrap/ng-bootstrap/util/util';

@Injectable({
  providedIn: 'root'
})
export class BackstoreService {

  public dealerData:BehaviorSubject<dealerVM[]> = new BehaviorSubject<dealerVM[]>([]);



  



  constructor() { }


 





  
}
