import { Component } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { RouterOutlet } from '@angular/router';


@Component({
    selector: 'app-mainpage',
    imports: [NavbarComponent, RouterOutlet,],
    templateUrl: './mainpage.component.html',
    styleUrl: './mainpage.component.css'
})
export class MainpageComponent {

}
