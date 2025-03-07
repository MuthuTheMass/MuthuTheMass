import { HttpClient, HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
    selector: 'app-sample-backend',
    imports: [],
    templateUrl: './sample-backend.component.html',
    styleUrl: './sample-backend.component.css'
})
export class SampleBackendComponent {

  vehicleDisplayData: BehaviorSubject<vehicleDetails> = new BehaviorSubject<vehicleDetails>({} as vehicleDetails);

  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.assignData();
  }

  assignData() {
    this.getData("User-1", "Vehicle-1").subscribe(r => {
      this.vehicleDisplayData.next( r.result as vehicleDetails);
    });
  }


  getData(userid: string, vehicleId: string):Observable<ApiResponse> {
    let params = new HttpParams()
      .set("UserId", userid)
      .set("VehileId", vehicleId);

    return this.http.get<ApiResponse>("https://localhost:7045/api/Vehicle/vehiclesingle", {params})
  }


}

export interface vehicleDetails {
         vehicleId :string
         vehicleName:string
         vehicleNumber:string
         vehicleImage:string
         driverName:string
         driverPhoneNumber:string
         vehicleModel:string
         alternative_Phone_Number:string
}

export interface ApiResponse {
  result: any;
  id: number;
  exception: any;
  status: number;
  isCanceled: boolean;
  isCompleted: boolean;
  isCompletedSuccessfully: boolean;
  creationOptions: number;
  asyncState: any;
  isFaulted: boolean;
}
