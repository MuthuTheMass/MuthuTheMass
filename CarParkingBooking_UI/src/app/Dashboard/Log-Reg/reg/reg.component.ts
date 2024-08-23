import { Component } from '@angular/core';
import { RegLogService } from '../../../reg-log.service';
import { routes } from '../../../app.routes';
import { Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrmcontrolValidationServiceService } from '../../service/ormcontrol-validation-service.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reg',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,CommonModule],
  templateUrl: './reg.component.html',
  styleUrl: './reg.component.css'
})
export class RegComponent {
validators : any;




register:any;
login:any;
regpage:any;
validate:any;
router:any;



constructor(public cs:RegLogService, _router :Router,private _validate:OrmcontrolValidationServiceService) {
    this.router = _router;
    this.validate = _validate;
    this.login=new FormGroup({
      useremail: new FormControl('',Validators.required),
      pass:new FormControl('',Validators.required),
    })




    this.regpage=new FormGroup({
        fullname:new FormControl('',Validators.required),
        regemail:new FormControl('',Validators.required),
        mobilenumber:new FormControl('',Validators.required),
        password:new FormControl('',Validators.required),
        confirmpassword:new FormControl('',Validators.required)
  
  
      })

  }


//   matchPassword(password: string, confirmPassword: string): boolean {
//     return password === confirmPassword;
//   }








    
    
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


    signin(){

        if(this.regpage.valid){
          this.router.navigate(['/home']);
        }
        else{
          this.checkValidityAndMarkAsTouched();
        }
      
    }


    logininto(){

        if(this.login.valid){
          this.router.navigate(['/home']);
        }
        else{
          this.checkValidityAndMarkAsTouched();
        }
      }
    checkValidityAndMarkAsTouched() {

    }

    
 



}
