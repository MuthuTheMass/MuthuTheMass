import { Component } from '@angular/core';
import { routes } from '../../../app.routes';
import { Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrmcontrolValidationServiceService } from '../../service/ormcontrol-validation-service.service';
import { CommonModule } from '@angular/common';
import { UserAuthService } from '../../../Service/Backend/user-auth.service';
import { DealerSignUp, Login, SignUp } from '../../../Service/Model/UserModels';
import { LoginResponse } from '../../../Service/Model/BackendUserModels';
import { HttpErrorResponse } from '@angular/common/http';
import { BackStoreService } from '../../../Service/store/back-store.service';

@Component({
  selector: 'app-dreg',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,CommonModule],
  templateUrl: './dreg.component.html',
  styleUrl: './dreg.component.css'
})
export class DregComponent {
  validators : any;

  dealerSignUpRotate:boolean = false;
  dealerSignInRotate: boolean = false;

  signinto() {
    throw new Error('Method not implemented.');
    }



  register:any;
  login:any;
  regpage:any;




  constructor( private router :Router,
    protected validate:OrmcontrolValidationServiceService,
    private auth:UserAuthService,
  private bsStore:BackStoreService) {


    }


  //   matchPassword(password: string, confirmPassword: string): boolean {
  //     return password === confirmPassword;
  //   }


ngOnInit(): void {
  
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







      registerBtn () {
        //TODO : Refactor to typescript
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
        //TODO: Refactor to typescript
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


      signUp(){
          if(this.regpage.valid){
            this.dealerSignUpRotate = true
            let signData ={
              name:this.regpage.value.fullname,
              email:this.regpage.value.regemail,
              phoneNo:this.regpage.value.mobilenumber,
              password:this.regpage.value.password,
            } as DealerSignUp

            this.auth.DealerSignUp(signData).toPromise()
            .then((response: boolean) => {
              console.log(response);
              this.dealerSignUpRotate = false;

            })
            .catch((error: HttpErrorResponse) => {
              console.error(error);
              this.dealerSignUpRotate = false;
            });

            this.loginBtn();
          }
          else{
            this. checkValidityAndMarkAsTouchedreg();
          }

      }


      logininto(){

          if(this.login.valid){
            this.dealerSignInRotate = true;
            let loginData ={
              email:this.login.value.useremail,
              password:this.login.value.pass,
            } as Login

            this.auth.DealerLogin(loginData).subscribe(
                          (data:LoginResponse) => {
                                localStorage.clear();
                                localStorage.setItem("Dealer",JSON.stringify(data));
                                this.bsStore.dealerLoggedData.set(data)
                                this.router.navigate(['/dhome']);
                                this.dealerSignInRotate = false;
                          },
                          (err:HttpErrorResponse) => {
                            console.log(err);
                            this.dealerSignInRotate = false;
                          },
                          
                        );;
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

