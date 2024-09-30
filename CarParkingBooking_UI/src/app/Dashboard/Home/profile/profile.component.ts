import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { EditdetailsComponent } from './editdetails/editdetails.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [EditdetailsComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
constructor(private router:Router){

}

 editdata(){


    this.router.navigate(['main/edit'])
}

}


