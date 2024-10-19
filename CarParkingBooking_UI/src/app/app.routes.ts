import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { RegComponent } from './Dashboard/Log-Reg/reg/reg.component';
import { MainComponent } from './Dashboard/Home/main/main.component';
import { MainpageComponent } from './Dashboard/Home/mainDashboard/mainpage.component';
import { NavbarComponent } from './Dashboard/Home/navbar/navbar.component';
import { ArticalComponent } from './Dashboard/Home/main/artical/artical.component';
import { BalajiarComponent } from './Dashboard/Home/main/balajiar/balajiar.component';
import { MuthubookComponent } from './Dashboard/Home/muthubook/muthubook.component';
import { UserdataComponent } from './Dashboard/Home/userdata/userdata.component';
import { EReceiptComponent } from './Dashboard/Home/e-receipt/e-receipt.component';
import { DregComponent } from './Dashboard/Dealer/dreg/dreg.component';
import { ProfileComponent } from './Dashboard/Home/profile/profile.component';
import { EditdetailsComponent } from './Dashboard/Home/profile/editdetails/editdetails.component';
import { SampleBackendComponent } from './sample-backend/sample-backend.component';

export const routes: Routes = [
  {
    path:"sampleback",component:SampleBackendComponent,title:"validation for the backend process only of balaji"
  },

{
    path:"",component:RegComponent,title:"CarParking-Login",pathMatch:"full"
},
{
   path:"Dreg",component:DregComponent,title:"Dealer-login"
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
            path:"profile",component:ProfileComponent,title:"profiledetailes",
               
       },
       {
            path:"edit",component:EditdetailsComponent,title:"editdetailes"
       },
       
        {
            path:"book",component:MuthubookComponent
        },
        {
            path:"user",component:UserdataComponent
        },
        {
            path:"erecepit",component:EReceiptComponent
        },
        {
            path:"nav",component:NavbarComponent,title:"navbar"
        },
    ]
}


];
