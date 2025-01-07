import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { dealerVM } from '../Model/dealermodal';
import { isNumber } from '@ng-bootstrap/ng-bootstrap/util/util';
import { userDetails } from '../Model/UserDetails';
import {VehicleModal} from "../Model/VehicleModal";

@Injectable({
  providedIn: 'root'
})
export class BackStoreService {

  public dealerData:BehaviorSubject<dealerVM[]> = new BehaviorSubject<dealerVM[]>([]);
  public userDetails:BehaviorSubject<userDetails> = new BehaviorSubject<userDetails>({} as userDetails);
  public VehicleData:BehaviorSubject<Array<VehicleModal>> = new BehaviorSubject<Array<VehicleModal>>([]);
  public VehicleDetail:BehaviorSubject<VehicleModal> = new BehaviorSubject<VehicleModal>({} as VehicleModal);





  constructor() { }









}
