import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { EditdetailsComponent } from './editdetails/editdetails.component';
import { CardComponent } from "../../../custom_components/card/card.component";
import { UserDetailsService } from '../../../Service/Backend/user-details.service';
import { BackStoreService } from '../../../Service/store/back-store.service';
import { LoginResponse } from '../../../Service/Model/UserModels';
import { VehicleDetialsService } from '../../../Service/Backend/vehicle-detials.service';
import { VehicleModal } from '../../../Service/Model/VehicleModal';
import { userDetails } from '../../../Service/Model/UserDetails';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [EditdetailsComponent, CardComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {


constructor(
  private router:Router,
  private userDetails:UserDetailsService,
  protected bsStore:BackStoreService,
  protected vehicleDetails:VehicleDetialsService ){

}

  ngOnInit(){
    const userData = localStorage.getItem("User");
    if(userData !== null){
      let data = JSON.parse(userData) as LoginResponse;
      this.userDetails.userFullDetails(data.email).subscribe(
        (response:userDetails) => {
          this.bsStore.userDetails.next(response);
        },
      );;
      this.vehicleDetails.halfVehicleDetailsByUserID(data.userID);
    }
  }


 editdata(){


    this.router.navigate(['main/edit'])
}
IScarData() {
  return this.bsStore.VehicleData.value.length >0;
}

userEdit() {
    this.router.navigate(["main/profile/edit",this.bsStore.userDetails.value.email])
  }


}


