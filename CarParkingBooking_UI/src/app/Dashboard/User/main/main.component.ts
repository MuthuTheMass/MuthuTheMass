import { Component, signal } from '@angular/core';
import { ArticalComponent } from './artical/artical.component';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { FormControl, FormGroup, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RatingModule } from 'ngx-bootstrap/rating';
import { DealerDataService } from '../../../Service/Backend/dealer-data.service';
import { BackStoreService } from '../../../Service/store/back-store.service';
import { routes } from '../../../app.routes';
import { dealerVM, UserDealerSearch } from '../../../Service/Model/dealermodal';
import { LoadingComponent } from '../../../shared/loading/loading.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [RatingModule, FormsModule, CommonModule, LoadingComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent {
  rating: number = 4.5;
  currentRate: number = 2;
  dealers = signal<UserDealerSearch[]>([]);
  totalItems = signal<number>(0);
  currentPage = signal<number>(1);
  itemsPerPage = signal<number>(10);
  data: number = 3;
  hoveringOver: any;

  constructor(
    public dealerDataService: DealerDataService,
    protected backStoreService: BackStoreService,
    private router: Router,
  ) {}

  get totalPages(): number {
    return Math.ceil(this.totalItems() / this.itemsPerPage());
  }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.dealerDataService
      .getalluserdata(this.currentPage(), this.itemsPerPage())
      .subscribe((data: any) => {
        this.dealers.set(data.data);
        this.totalItems.set(data.totalDataCount);
      });
  }

  onPageChange(page: number) {
    this.currentPage.set(page);
    this.fetchData();
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

  BookingSlot(DealerID: string) {
    sessionStorage.setItem('dealerID', DealerID);
    this.router.navigate(['/main/dealer-details'], {
      state: { DealerID },
    });
  }
}
