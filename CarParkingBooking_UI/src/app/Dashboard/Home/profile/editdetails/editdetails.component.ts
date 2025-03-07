import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BackStoreService } from '../../../../Service/store/back-store.service';
import { UserDetailsService } from '../../../../Service/Backend/user-details.service';
import {ActivatedRoute, Router} from '@angular/router';
import {userDetails, UserUpdateData} from '../../../../Service/Model/UserDetails';
import {UserHelperService} from "../../../../Service/UIService/user-helper.service";

@Component({
    selector: 'app-editdetails',
    imports: [ReactiveFormsModule],
    templateUrl: './editdetails.component.html',
    styleUrl: './editdetails.component.css'
})
export class EditdetailsComponent {

    editDetail!: FormGroup;

constructor(
    protected bsStore:BackStoreService,
    protected userService:UserDetailsService,
    private route: ActivatedRoute,
    private router:Router,
    private userHelp:UserHelperService){
}

ngOnInit(){
    this.editDetail = new FormGroup({
        Name:new FormControl(),
        Email:new FormControl(),
        Mobile:new FormControl(),
        Address:new FormControl(),
        Image:new FormControl()
    });
    this.IsDetailsAvailable();
}

//  profileimage(){
//     // Get the image element (assuming it is an HTMLImageElement)
//     const profilepic = document.getElementById('imgefile') as HTMLImageElement;
//
//     // Ensure profilechange is an HTMLInputElement
//     const profilechange = document.getElementById('input-filed') as HTMLInputElement;
//
//     // Check if files exist and take the first one
//     if (profilechange.files && profilechange.files.length > 0) {
//         // Set the src of the image element to the selected file
//         profilepic.src = URL.createObjectURL(profilechange.files[0]);
//     } else {
//         console.error("No file selected");
//     }
// };

IsDetailsAvailable(){
    if( this.bsStore.userDetails.getValue() != null && this.bsStore.userDetails.getValue().email != null){
      this.setEditDetailForm(this.bsStore.userDetails.getValue());
    }
    else{
        const userEmail = this.route.snapshot.paramMap.get('emailid');
        this.userService.userFullDetails(userEmail || '').subscribe(
            (response:userDetails) => {
                this.bsStore.userDetails.next(response);
                this.setEditDetailForm(response);
            },
          );


    }
}

  private setEditDetailForm(details: userDetails) {
    this.editDetail.patchValue({
      Name: details.name,
      Email: details.email,
      Mobile: details.mobileNumber,
      Address: details.address,
      Image: details.profilePicture
    });
  }

userImage(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0] as Blob;

    const reader = new FileReader();
      reader.onload = () => {
        let result = reader.result as string
        if(result.includes(',')){
          result = result.split(',')[1]
        }
        this.editDetail.controls['Image'].setValue(result);
      };
    reader.readAsDataURL(file);
}

defaultPhoto(){
  let photo: string;
  let y;
  if(this.editDetail.controls['Image'].value != null ){
     y = this.editDetail.controls['Image'].value.split(',')[0]
  }

  photo = this.editDetail.controls['Image'].value != null ? 'data:image/jpeg;base64,' + this.editDetail.controls['Image'].value : 'https://bootdey.com/img/Content/avatar/avatar7.png';
  return photo;
}


  UpdateConfirm(){

    const file = this.userHelp.base64ToFile(this.editDetail.controls['Image'].value, this.editDetail.controls['Name'].value+'.png');

    let formData = new FormData();
    formData.append('ProfilePicture', file);
    formData.append('Name', this.editDetail.controls['Name'].value);
    formData.append('Email', this.editDetail.controls['Email'].value);
    formData.append('MobileNumber', this.editDetail.controls['Mobile'].value);
    formData.append('Address', this.editDetail.controls['Address'].value);

    this.userService.UpdateData(formData).subscribe(
      (response:boolean) => {
          console.log(response);
      }
    );

    this.router.navigate(["main/profile"])
  }

  cancel(){
  this.router.navigate(["main/profile"])
  }
}
