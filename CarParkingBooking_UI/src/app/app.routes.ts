import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { RegComponent } from './Dashboard/Log-Reg/reg/reg.component';
import { MainComponent } from './Dashboard/Home/main/main.component';
import { MainpageComponent } from './Dashboard/Home/mainDashboard/mainpage.component';
import { NavbarComponent } from './Dashboard/Home/navbar/navbar.component';

export const routes: Routes = [

{
    path:"",component:RegComponent,title:"CarParking-Login",pathMatch:"full"
},



{
    path:"main",component:MainpageComponent,title:"mainpage",children :[
        {
            path:"",component:MainComponent,title:"homepage"
        },
        {
            path:"nav",component:NavbarComponent,title:"navbar"
        }
    ]
}


];
