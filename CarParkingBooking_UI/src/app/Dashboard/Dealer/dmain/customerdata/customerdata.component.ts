import { Component } from '@angular/core';
import {userDetailsForDealer} from "../../../../Service/Model/UserDetails";
import {UserDetailsService} from "../../../../Service/Backend/user-details.service";

@Component({
  selector: 'app-customerdata',
  standalone: true,
  imports: [],
  templateUrl: './customerdata.component.html',
  styleUrl: './customerdata.component.css'
})
export class CustomerdataComponent {
  userDetails: userDetailsForDealer[] = [] as userDetailsForDealer[];

  constructor(private userService: UserDetailsService) { }

  ngOnInit() {
    this.userService.GetAllUsers().subscribe(
      (result:any) => {
        this.userDetails = result;
      }
    )
  }
}
