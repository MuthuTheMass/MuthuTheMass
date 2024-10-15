import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrmcontrolValidationServiceService {




  constructor() { }


  public validation(controls:any,controllerName:string){
    let valid=controls.get(controllerName)
 
    if(valid.errors?.required && valid.touched){
  return true;
    }
    return false;
}
}
