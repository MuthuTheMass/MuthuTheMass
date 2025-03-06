import {Component, computed} from '@angular/core';
import {UserDetailsForDealer} from "../../../../Service/Model/UserDetails";
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
export class CustomerdataComponent {
  userDetails: UserDetailsForDealer[] = [] as UserDetailsForDealer[];

  constructor(
    private userService: UserDetailsService,
    private dealerService: DealerDataService,
    private bsStore:BackStoreService,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.getNewusersForDealer();
  }


  getNewusersForDealer(){
    if(this.bsStore.dealerLoggedData().email == undefined){
      var dealerData = localStorage.getItem("Dealer");
      this.bsStore.dealerLoggedData.set(dealerData as any);
      if(dealerData != null){
        this.dealerService.getNewUsers(JSON.parse(dealerData).email).subscribe(
          (result:any) => {
            this.userDetails = result;
          },
          (err:any) => {
            console.log(err);
          }
        )
      }
    }
  }

  convertionOFImage(image:any){
    const reader = new FileReader();
    reader.readAsDataURL(image);
    reader.onloadend = () => {
      return this.sanitizer.bypassSecurityTrustUrl(reader.result as string);
    };
  }
}
