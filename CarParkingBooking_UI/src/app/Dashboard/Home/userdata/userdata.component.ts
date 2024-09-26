import { Component } from '@angular/core';

@Component({
  selector: 'app-userdata',
  standalone: true,
  imports: [],
  templateUrl: './userdata.component.html',
  styleUrl: './userdata.component.css'
})
export class UserdataComponent {

  vechilemodal(){
    document.getElementById("myDropdown")!.classList.toggle("show");
  }
  
  filterFunction() {
    const input: HTMLInputElement | null = document.getElementById("myInput") as HTMLInputElement;
    const filter: string = input.value.toUpperCase();
    const div: HTMLElement | null = document.getElementById("myDropdown");
    const a: HTMLAnchorElement[] = Array.from(div!.getElementsByTagName("a"));
    
    for (let i: number = 0; i < a.length; i++) {
      const txtValue: string = a[i].textContent || a[i].innerText || "";
      if (txtValue.toUpperCase().indexOf(filter) > -1) {
        a[i].style.display = "";
      } else {
        a[i].style.display = "none";
      }
    }
  }




}
