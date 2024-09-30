import { Component } from '@angular/core';

@Component({
  selector: 'app-editdetails',
  standalone: true,
  imports: [],
  templateUrl: './editdetails.component.html',
  styleUrl: './editdetails.component.css'
})
export class EditdetailsComponent {





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




}
