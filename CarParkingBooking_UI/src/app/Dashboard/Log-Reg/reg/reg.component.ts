import { Component } from '@angular/core';
import { RegLogService } from '../../../reg-log.service';
import { routes } from '../../../app.routes';
import { Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrmcontrolValidationServiceService } from '../../service/ormcontrol-validation-service.service';
import { CommonModule } from '@angular/common';
import { UserAuthService } from '../../../Service/Backend/user-auth.service';
import {} from '@angular/common/http';
import { Login, SignUp } from '../../../Service/Model/UserModels';

@Component({
  selector: 'app-reg',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,CommonModule],
  templateUrl: './reg.component.html',
  styleUrl: './reg.component.css'
})
export class RegComponent {
validators : any;
signinto() {
  throw new Error('Method not implemented.');
  }



register:any;
login:any;
regpage:any;
validate:any;
router:any;



constructor(public cs:RegLogService, _router :Router,private _validate:OrmcontrolValidationServiceService , private Login:UserAuthService) {
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
          let data = { 
            userName:this.regpage.value.fullname,
            email:this.regpage.value.regemail,
            mobileNumber: this.regpage.value.mobilenumber,
            password: this.regpage.value.password,
          } as SignUp


          this.Login.SignUp(data).then(
            (response: any) => {
              console.log(response);
              this.loginBtn();
            },
            (error: any) => {
              console.error(error);
            }
          )
        }
        else{
          this. checkValidityAndMarkAsTouchedreg();
        }

    }


    logininto(){

        if(this.login.valid){
          let data = {
            email:this.login.value.useremail,
            password:this.login.value.pass
          } as Login;

          this.Login.Login(data);
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
