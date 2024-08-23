import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { RegComponent } from './Dashboard/Log-Reg/reg/reg.component';
import { MainComponent } from './Dashboard/Home/main/main.component';

export const routes: Routes = [

{
    path:"",component:RegComponent,title:"CarParking-Login",pathMatch:"full"
},{
    path:"home",component:MainComponent
}


];
