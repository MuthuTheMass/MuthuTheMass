import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { routes } from '../../../../app.routes';


@Component({
  selector: 'app-artical',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './artical.component.html',
  styleUrl: './artical.component.css'
})
export class ArticalComponent {

constructor( private router:Router){

}
userbooking(){


  this.router.navigate(['/main/uservehicle']);

}


}
