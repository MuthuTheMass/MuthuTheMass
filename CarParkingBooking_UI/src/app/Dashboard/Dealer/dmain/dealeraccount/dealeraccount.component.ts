import { Component } from '@angular/core';
import {BackStoreService} from "../../../../Service/store/back-store.service";
import {DealerDataService} from "../../../../Service/Backend/dealer-data.service";

@Component({
  selector: 'app-dealeraccount',
  standalone: true,
  imports: [],
  templateUrl: './dealeraccount.component.html',
  styleUrl: './dealeraccount.component.css'
})
export class DealeraccountComponent {

  constructor(
    protected stateStore:BackStoreService,
    protected dealerService:DealerDataService,
  ) {
  }

  ngOnInit() {
    this.dealerService.getalluserdata()
  }
}
