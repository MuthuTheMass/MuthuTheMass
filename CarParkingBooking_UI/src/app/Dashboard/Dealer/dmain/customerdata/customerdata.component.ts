import {Component, OnInit} from '@angular/core';
import {DashboardDetailsForDealer, UserDetailsForDealer} from "../../../../Service/Model/UserDetails";
import {UserDetailsService} from "../../../../Service/Backend/user-details.service";
import {DealerDataService} from "../../../../Service/Backend/dealer-data.service";
import {BackStoreService} from "../../../../Service/store/back-store.service";
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-customerdata',
  standalone: true,
  imports: [],
  templateUrl: './customerdata.component.html',
  styleUrl: './customerdata.component.css'
})
export class CustomerdataComponent implements OnInit {
  DealerSlots:DashboardDetailsForDealer = { newCustomers:[] as UserDetailsForDealer[]} as DashboardDetailsForDealer;

  constructor(
    private userService: UserDetailsService,
    private dealerService: DealerDataService,
    private bsStore:BackStoreService,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.getDealerDashboard();
  }


  getDealerDashboard(){
    if(this.bsStore.dealerLoggedData().email == undefined){
      var dealerData = localStorage.getItem("Dealer");
      this.bsStore.dealerLoggedData.set(dealerData as any);
    }
    if(this.bsStore.dealerLoggedData() != null){
      this.dealerService.getNewUsers(this.bsStore.dealerLoggedData().email).subscribe(
        async (result:DashboardDetailsForDealer) => {
          this.DealerSlots = result;
          this.DealerSlots.newCustomers?.forEach(element => {
            if(element.picture != null){
              element.picture = `data:image/jpeg;base64,${element.picture}`;
            }});

          
        },
        (err:any) => {
          console.log(err);
        }
      )
    }
  }

  convertToBase64(imageBlob: Blob) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(imageBlob);
      reader.onloadend = () => {
        resolve(reader.result); // Base64 string
      };
      reader.onerror = (error) => reject(error);
    });
  }
}
