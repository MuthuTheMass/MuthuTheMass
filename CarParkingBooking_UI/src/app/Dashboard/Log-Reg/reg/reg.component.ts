import { Component } from '@angular/core';

@Component({
  selector: 'app-reg',
  standalone: true,
  imports: [],
  templateUrl: './reg.component.html',
  styleUrl: './reg.component.css'
})
export class RegComponent {

    
    
    registerBtn () {
        const container: HTMLElement | null = document.getElementById('container');
    const registerBtn: HTMLElement | null = document.getElementById('register');
    const loginBtn: HTMLElement | null = document.getElementById('login');


        registerBtn?.addEventListener('click', () => {
            if (container) {
                container.classList.add("active");
            }
        });
    }
       
    loginBtn() {

        const container: HTMLElement | null = document.getElementById('container');
    const registerBtn: HTMLElement | null = document.getElementById('register');
    const loginBtn: HTMLElement | null = document.getElementById('login');

        loginBtn?.addEventListener('click', () => {
            if (container) {
                container.classList.remove("active");
            }
        });
    }



// signUP(){
// const   container = document.getElementById('container');
//  const registerBtn = document.getElementById('register');
//  const loginBtn = document.getElementById('login');

 
//  registerBtn?.classList.add('active')
// }

// logIn(){
//     const   container = document.getElementById('container');
//     const registerBtn = document.getElementById('register');
//     const loginBtn = document.getElementById('login');


//    registerBtn?.classList.remove('active')  
// }


  

//  container = document.getElementById('container');
//  registerBtn = document.getElementById('register');
//   loginBtn = document.getElementById('login');

//   registerBtn.addEventListener('click', ()
// => {
//   ControlContainer.classList.add("active");

// });

// loginBtn.addEventListener('click', ()
// => {
//   ControlContainer.classList.remove("active");

// });
// }

}
