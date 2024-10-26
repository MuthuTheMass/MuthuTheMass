import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { EditdetailsComponent } from './editdetails/editdetails.component';
import { CardComponent } from "../../../custom_components/card/card.component";
import { UserDetailsService } from '../../../Service/Backend/user-details.service';
import { BackstoreService } from '../../../Service/store/backstore.service';
import { LoginResponse } from '../../../Service/Model/UserModels';
import { userDetails } from '../../../Service/Model/UserDetails';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [EditdetailsComponent, CardComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {



constructor(private router:Router,private userDetails:UserDetailsService,protected bsStore:BackstoreService){

}

  ngOnInit(){
    const userData = localStorage.getItem("User");
    if(userData !== null){
      let data = JSON.parse(userData);
      this.userDetails.userFullDetails(data.email);
    }
  }


 editdata(){


    this.router.navigate(['main/edit'])
}

}


