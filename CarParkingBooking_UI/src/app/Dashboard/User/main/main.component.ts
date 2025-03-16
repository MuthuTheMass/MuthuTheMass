import { Component, signal } from '@angular/core';
import { ArticalComponent } from "./artical/artical.component";
import {Router, RouterLink, RouterOutlet} from '@angular/router';
import {FormControl, FormGroup, FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import {RatingModule} from "ngx-bootstrap/rating";
import {DealerDataService} from "../../../Service/Backend/dealer-data.service";
import {BackStoreService} from "../../../Service/store/back-store.service";
import {routes} from "../../../app.routes";
import { dealerVM } from '../../../Service/Model/dealermodal';



@Component({
  selector: 'app-main',
  standalone: true,
  imports: [RatingModule, FormsModule, CommonModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {


  rating: number = 4.5;
  currentRate: number = 2;
  dealers = signal<dealerVM[]>([]);
  totalItems = signal<number>(0);
  currentPage = signal<number>(1);
  itemsPerPage = signal<number>(10);


  constructor(public dealerDataService: DealerDataService, protected backStoreService: BackStoreService, private  router: Router) {

  }

  ngOnInit():void {
    this.fetchData();
  }

  fetchData() {
    this.dealerDataService.getalluserdata(this.currentPage(), this.itemsPerPage()).subscribe((data: any) => 
      {
        this.dealers.set(data.data);
        this.totalItems.set(data.totalDataCount);
      });
  }

  onPageChange(page: number) {
    this.currentPage.set(page);
    this.fetchData();
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems() / this.itemsPerPage());
  }

  convertStringToFloat(value: string) {
    return parseFloat(value);
  }




  getdata() {
    const searchbox = document.getElementById('loac') as HTMLInputElement;
    const putdata = document.getElementById('sdata');
    const sbtn = document.getElementById('btnd');

    if (putdata) {
      putdata.innerHTML = searchbox?.value;
    }
  }

  ariaValueText(current: number, max: number) {
    return `${current} out of ${max} hearts`;
  }

  data: number = 3;
  hoveringOver: any;


  dealerdata(){
    this.router.navigate(['/main/dealer-details']);
  }
}
