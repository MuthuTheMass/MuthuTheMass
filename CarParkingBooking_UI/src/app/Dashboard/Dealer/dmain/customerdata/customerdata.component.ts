import {Component, computed} from '@angular/core';
import {userDetailsForDealer} from "../../../../Service/Model/UserDetails";
import {UserDetailsService} from "../../../../Service/Backend/user-details.service";
import {DealerDataService} from "../../../../Service/Backend/dealer-data.service";
import {BackStoreService} from "../../../../Service/store/back-store.service";

@Component({
  selector: 'app-customerdata',
  standalone: true,
  imports: [],
  templateUrl: './customerdata.component.html',
  styleUrl: './customerdata.component.css'
})
export class CustomerdataComponent {
  userDetails: userDetailsForDealer[] = [] as userDetailsForDealer[];

  constructor(
    private userService: UserDetailsService,
    private dealerService: DealerDataService,
    private bsStore:BackStoreService) { }

  ngOnInit() {
    this.dealerService.getNewUsers(this.bsStore.dealerLoggedData().email).subscribe(
      (result:any) => {
        this.userDetails = result;
      }
    )
  }
}
