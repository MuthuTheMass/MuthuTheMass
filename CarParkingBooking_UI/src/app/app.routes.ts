import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { RegComponent } from './Dashboard/Log-Reg/reg/reg.component';
import { MainComponent } from './Dashboard/Home/main/main.component';
import { MainpageComponent } from './Dashboard/Home/mainDashboard/mainpage.component';
import { NavbarComponent } from './Dashboard/Home/navbar/navbar.component';
import { ArticalComponent } from './Dashboard/Home/main/artical/artical.component';
import { BalajiarComponent } from './Dashboard/Home/main/balajiar/balajiar.component';
import { MuthubookComponent } from './Dashboard/Home/muthubook/muthubook.component';

export const routes: Routes = [

{
    path:"",component:RegComponent,title:"CarParking-Login",pathMatch:"full"
},



{
    path:"main",component:MainpageComponent,title:"mainpage",children :[
        {
            path:"",component:MainComponent,title:"homepage",children:[
                {
                    path:"artical",component:ArticalComponent
                },
                {
                    path:"balaji",component:BalajiarComponent
                }
            ]
        },
        {
            path:"book",component:MuthubookComponent
        },
        {
            path:"nav",component:NavbarComponent,title:"navbar"
        },
    ]
}


];
