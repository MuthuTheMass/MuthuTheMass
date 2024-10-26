import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { EditdetailsComponent } from './editdetails/editdetails.component';
import { CardComponent } from "../../../custom_components/card/card.component";

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [EditdetailsComponent, CardComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
constructor(private router:Router){

}

  ngOnInit(){

  }


 editdata(){


    this.router.navigate(['main/edit'])
}

}


