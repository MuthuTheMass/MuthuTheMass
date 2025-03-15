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
import { PagenotfoundComponent } from './custom_components/pagenotfound/pagenotfound.component';
import { DmainComponent } from './Dashboard/Dealer/dmain/dmain.component';
import { CustomerdataComponent } from './Dashboard/Dealer/dmain/customerdata/customerdata.component';
import { OfflinebookingComponent } from './Dashboard/Dealer/dmain/offlinebooking/offlinebooking.component';
import { OfflineReceptComponent } from './Dashboard/Dealer/dmain/offline-recept/offline-recept.component';
import { BookingHistoryComponent } from './Dashboard/Dealer/dmain/booking-history/booking-history.component';
import { PaymentHistoryComponent } from './Dashboard/Dealer/dmain/payment-history/payment-history.component';
import { DealeraccountComponent } from './Dashboard/Dealer/dmain/dealeraccount/dealeraccount.component';
import { EditDealerdataComponent } from './Dashboard/Dealer/dmain/edit-dealerdata/edit-dealerdata.component';
import { AllPaymentComponent } from './Dashboard/Dealer/dmain/all-payment/all-payment.component';
import { ZenparkAboutComponent } from './Dashboard/home-navebar/zenpark-about/zenpark-about.component';
import { UserBookingHistoryComponent } from './Dashboard/home-navebar/user-booking-history/user-booking-history.component';
import { UserConfirmBookingComponent } from './Dashboard/Home/main/user-confirm-booking/user-confirm-booking.component';
import {
  UserPaymentHistoryComponent
} from "./Dashboard/home-navebar/user-payment-history/user-payment-history.component";
import {DealeDetailsComponent} from "./Dashboard/Home/main/deale-details/deale-details.component";


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
  path:"dhome",component:DmainComponent,title:"dealermain page",children:[
    {
        path:"",component:CustomerdataComponent,title:"home page"
    },{
        path:"off-booking",component:OfflinebookingComponent,title:"offline-booking for customer"
    },{
        path:"offline-bill",component:OfflineReceptComponent,title:"customer offline bill"
    },{
        path:"booking-details",component:BookingHistoryComponent,title:"customer booking history"
    },{
        path:"payment-details",component:PaymentHistoryComponent,title:"customer payment history"
    },{
        path:"dealer-account",component:DealeraccountComponent,title:"dealer detailes"
    },{
        path:"editdealer",component:EditDealerdataComponent,title:"dealerdata"
    },{
        path:"allpayment",component:AllPaymentComponent,title:"dealerpayment"
    }
  ]
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
            path:"about",component:ZenparkAboutComponent,title:"zenparkabout-info"
        },
        {
             path:"customerhistory",component:UserBookingHistoryComponent,title:"user-booking-info-"
        },
    {
      path: "userpaymenthistory",component:UserPaymentHistoryComponent,title:"user-payment-history"
    },
        {
            path:"profile",component:ProfileComponent,title:"profiledetailes",
        },
        {
            path:"profile/edit/:emailid",component:EditdetailsComponent,title:"editdetailes"
        },

        {
            path:"book",component:MuthubookComponent
        },
    {
      path:"dealer-details",component:DealeDetailsComponent,title:"dealerdetails"
    },
        {
            path:"uservehicle",component:UserdataComponent
        },
        {
            path:"erecepit",component:EReceiptComponent
        },{
            path:"confirmbooking",component:UserConfirmBookingComponent,title:"user-confirm-booking"
        }


    ]
},
  {
    path:"**",component:PagenotfoundComponent,title:"something went wrong"
  },


];
