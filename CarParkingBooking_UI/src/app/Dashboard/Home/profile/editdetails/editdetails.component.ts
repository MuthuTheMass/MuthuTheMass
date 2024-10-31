import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BackstoreService } from '../../../../Service/store/backstore.service';
import { UserDetailsService } from '../../../../Service/Backend/user-details.service';
import { ActivatedRoute } from '@angular/router';
import { userDetails } from '../../../../Service/Model/UserDetails';

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
    private route: ActivatedRoute){
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

 profileimage(){
    // Get the image element (assuming it is an HTMLImageElement)
    const profilepic = document.getElementById('imgefile') as HTMLImageElement;

    // Ensure profilechange is an HTMLInputElement
    const profilechange = document.getElementById('input-filed') as HTMLInputElement;

    // Check if files exist and take the first one
    if (profilechange.files && profilechange.files.length > 0) {
        // Set the src of the image element to the selected file
        profilepic.src = URL.createObjectURL(profilechange.files[0]);
    } else {
        console.error("No file selected");
    }
};

IsDetailsAvailable(){
    if( this.bsStore.userDetails.getValue() != null && this.bsStore.userDetails.getValue().email != null){
        this.editDetail.controls['Name'].setValue(this.bsStore.userDetails.getValue().name);
        this.editDetail.controls['Email'].setValue(this.bsStore.userDetails.getValue().email);
        this.editDetail.controls['Mobile'].setValue(this.bsStore.userDetails.getValue().mobileNumber);
        this.editDetail.controls['Address'].setValue(this.bsStore.userDetails.getValue().address)
        this.editDetail.controls['Image'].setValue(this.bsStore.userDetails.getValue().address)
    }
    else{
        const userEmail = this.route.snapshot.paramMap.get('emailid');
        this.userService.userFullDetails(userEmail || '').subscribe(
            (response:userDetails) => {
                this.bsStore.userDetails.next(response);
                this.editDetail.controls['Name'].setValue(this.bsStore.userDetails.getValue().name);
                this.editDetail.controls['Email'].setValue(this.bsStore.userDetails.getValue().email);
                this.editDetail.controls['Mobile'].setValue(this.bsStore.userDetails.getValue().mobileNumber);
                this.editDetail.controls['Address'].setValue(this.bsStore.userDetails.getValue().address)
                this.editDetail.controls['Image'].setValue(this.bsStore.userDetails.getValue().address)
            },
          );;
        

    }
}

userImage(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0] as Blob;

    const reader = new FileReader();
      reader.onload = () => {
        this.editDetail.controls['Image'].setValue( reader.result as string);
      };
    reader.readAsDataURL(file);
}
}
