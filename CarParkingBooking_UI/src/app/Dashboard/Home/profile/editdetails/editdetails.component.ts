import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BackstoreService } from '../../../../Service/store/backstore.service';
import { UserDetailsService } from '../../../../Service/Backend/user-details.service';
import {ActivatedRoute, Router} from '@angular/router';
import {userDetails, UserUpdateData} from '../../../../Service/Model/UserDetails';
import {UserHelperService} from "../../../../Service/UIService/user-helper.service";

@Component({
  selector: 'app-editdetails',
  standalone: true,
  imports: [ReactiveFormsModule  ],
  templateUrl: './editdetails.component.html',
  styleUrl: './editdetails.component.css'
})
export class EditdetailsComponent {

    editDetail!: FormGroup;

constructor(
    protected bsStore:BackstoreService,
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
        this.editDetail.controls['Image'].setValue( reader.result as string);
      };
    reader.readAsDataURL(file);
}

defaultPhoto(){
  let photo: string;
  photo = this.editDetail.controls['Image'].value != null ? 'data:image/jpeg;base64,' + this.editDetail.controls['Image'].value : 'https://bootdey.com/img/Content/avatar/avatar7.png';
  return photo;
}


  UpdateConfirm(){

    const file = this.userHelp.base64ToFile(this.editDetail.controls['Image'].value, this.editDetail.controls['Name'].value+'.png');

    const formData = new FormData();
    formData.append('file', file);

    let updateData={
      Name:this.editDetail.controls['Name'].value,
      Email:this.editDetail.controls['Email'].value,
      MobileNumber:this.editDetail.controls['Mobile'].value,
      Address:this.editDetail.controls['Address'].value,
      ProfilePicture:file
    } as UserUpdateData;

    this.userService.UpdateData(updateData).subscribe(
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
