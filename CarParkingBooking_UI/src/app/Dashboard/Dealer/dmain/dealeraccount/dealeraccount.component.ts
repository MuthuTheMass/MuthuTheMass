import { Component } from '@angular/core';

@Component({
  selector: 'app-dealeraccount',
  standalone: true,
  imports: [],
  templateUrl: './dealeraccount.component.html',
  styleUrl: './dealeraccount.component.css'
})
export class DealeraccountComponent {


  // Select DOM elements
 buttons = document.querySelectorAll<HTMLButtonElement>(".card-buttons button");
 sections = document.querySelectorAll<HTMLElement>(".card-section");
 card = document.querySelector<HTMLElement>(".card");

// Define event handler
   handleButtonClick = (e: Event): void => {
  const target = e.target as HTMLButtonElement;
  const targetSection = target.getAttribute("data-section");
  
  if (!targetSection) return;

  const section = document.querySelector<HTMLElement>(targetSection);

  if (!section) return;

  // Update card state and class based on target section
  if (targetSection !== "#about") {
    this.card?.classList.add("is-active");
  } else {
    this.card?.classList.remove("is-active");
  }

  this.card?.setAttribute("data-state", targetSection);

  // Update active state for sections and buttons
  this.sections.forEach(s => s.classList.remove("is-active"));
  this.buttons.forEach(b => b.classList.remove("is-active"));

  target.classList.add("is-active");
  section.classList.add("is-active");
};

// Add event listeners to buttons

buttonforevent(){
  this.buttons.forEach(btn => {
    btn.addEventListener("click", this.handleButtonClick);
  });
  
}



}
