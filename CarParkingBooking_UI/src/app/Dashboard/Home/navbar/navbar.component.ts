import { Component } from '@angular/core';

@Component({
    selector: 'app-navbar',
    imports: [],
    templateUrl: './navbar.component.html',
    styleUrl: './navbar.component.css'
})
export class NavbarComponent {

getdata(){
  const searchbox= document.getElementById('loac') as HTMLInputElement;
  const putdata = document.getElementById('sdata');
  const sbtn = document.getElementById('btnd');
  
  
    if (putdata) {
      putdata.innerHTML = searchbox?.value;
    }
}

}
