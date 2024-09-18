import { Component } from '@angular/core';
import { ArticalComponent } from "./artical/artical.component";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [ArticalComponent,RouterOutlet],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

  /**
   *
   */
  constructor() {
    
  }

  ngOnInit(){
    
  }

  getdata(){
    const searchbox= document.getElementById('loac') as HTMLInputElement;
    const putdata = document.getElementById('sdata');
    const sbtn = document.getElementById('btnd');
    
    
      if (putdata) {
        putdata.innerHTML = searchbox?.value;
      }
  }
}
