import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-dealeraccount',
  standalone: true,
  imports: [],
  templateUrl: './dealeraccount.component.html',
  styleUrls: ['./dealeraccount.component.css'] // Fixed typo from "styleUrl" to "styleUrls"
})
export class DealeraccountComponent {

  constructor(private router: Router) { }

  ngAfterViewInit() { // Moved DOM-related code to ngAfterViewInit lifecycle hook
    const buttons = document.querySelectorAll(".card-buttons button");
    const sections = document.querySelectorAll(".card-section");
    const card = document.querySelector(".card");

    const handleButtonClick = (e: Event) => {
      const target = e.target as HTMLElement;
      const targetSection = target.getAttribute("data-section");
      const section = targetSection ? document.querySelector(targetSection) : null;

      if (targetSection !== "#about") {
        card?.classList.add("is-active");
      } else {
        card?.classList.remove("is-active");
      }

      card?.setAttribute("data-state", targetSection || "");
      sections.forEach(s => s.classList.remove("is-active"));
      buttons.forEach(b => b.classList.remove("is-active"));
      target.classList.add("is-active");
      section?.classList.add("is-active");
    };

    buttons.forEach(btn => {
      btn.addEventListener("click", handleButtonClick);
    });
  }

dealereditprofile(){
  this.router.navigate(['/dhome/editdealer']);
}


}
