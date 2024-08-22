import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { RegComponent } from './Dashboard/Log-Reg/reg/reg.component';

export const routes: Routes = [

{
    path:"",component:RegComponent,title:"CarParking-Login",pathMatch:"full"
}


];
