import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  
  if(localStorage.getItem("usertoken")){
    return true;
  }
  return false;
};
