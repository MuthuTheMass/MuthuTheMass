import { Component } from '@angular/core';
import { RegLogService } from '../../../reg-log.service';
import { routes } from '../../../app.routes';
import { Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrmcontrolValidationServiceService } from '../../service/ormcontrol-validation-service.service';
import { CommonModule } from '@angular/common';
import { UserAuthService } from '../../../Service/Backend/user-auth.service';

@Component({
  selector: 'app-dreg',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,CommonModule],
  templateUrl: './dreg.component.html',
  styleUrl: './dreg.component.css'
})
export class DregComponent {
  validators : any;
  signinto() {
    throw new Error('Method not implemented.');
    }
  
  
  
  register:any;
  login:any;
  regpage:any;
  validate:any;
  router:any;
  
  
  
  constructor(public cs:RegLogService, _router :Router,private _validate:OrmcontrolValidationServiceService,private http:UserAuthService) {
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
          const container = document.getElementById('container');
      const registerBtn  = document.getElementById('register');
      const loginBtn = document.getElementById('login');
  
        container?.classList.add('active');
          // registerBtn?.addEventListener('click', () => {
          //     if (container) {
          //         container.classList.add("active");
          //     }
          // });
      }
         
      loginBtn() {
  
          const container  = document.getElementById('container');
      const registerBtn = document.getElementById('register');
      const loginBtn = document.getElementById('login');
  
  
          container?.classList.remove("active");
          // loginBtn?.addEventListener('click', () => {
          //     if (container) {
          //         container.classList.remove("active");
          //     }
          // });
      }
  
  
      signin(){
  
          if(this.regpage.valid){
            this.router.navigate(['/main']);
          }
          else{
            this. checkValidityAndMarkAsTouchedreg();
          }
        
      }
  
  
      logininto(){
  
          if(this.login.valid){

            this.router.navigate(['/main']);
          }
          else{
            this.checkValidityAndMarkAsTouched();
          }
        }
     
  
      
   
  
      checkValidityAndMarkAsTouched(): void {
        // Loop through all form controls and mark them as touched
        Object.keys(this.login.controls).forEach((controlName) => {
          const control = this.login.get(controlName);
          if (control) {
            control.markAsTouched();
          }
        });
      }
      checkValidityAndMarkAsTouchedreg(): void {
        // Loop through all form controls and mark them as touched
        Object.keys(this.regpage.controls).forEach((controlName) => {
          const control = this.regpage.get(controlName);
          if (control) {
            control.markAsTouched();
          }
        });
      }
  
  
  }
  
