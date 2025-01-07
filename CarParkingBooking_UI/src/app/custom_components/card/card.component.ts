import {Component, EventEmitter, Input,  Output,} from '@angular/core';
import {miniVehicleModal,} from '../../Service/Model/VehicleModal';
import { Router } from '@angular/router';
import { BackStoreService } from '../../Service/store/back-store.service';

@Component({
  selector: 'app-card',
  standalone: true,
  imports: [],
  templateUrl: './card.component.html',
  styleUrl: './card.component.css'
})
export class CardComponent {

@Input("data") data:miniVehicleModal | undefined;
@Output() editDetails:EventEmitter<string> = new EventEmitter<string>();

constructor(
  private _route:Router,
  private _backStore:BackStoreService

) {


}

ngOnInit(): void {
}

navigateEditVehicle(vehicleNumber:string | undefined) {
  let emailId:string = this._backStore.userDetails.getValue().userID;
  this._route.navigate(['main/uservehicle'],{ queryParams: { emailId: emailId, vehicleId: vehicleNumber } });

  }

}
