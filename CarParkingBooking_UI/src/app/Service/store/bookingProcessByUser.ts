import { Injectable, signal } from '@angular/core';
import { BookingDto } from '../Model/BookingDealerModal';

@Injectable({
  providedIn: 'root',
})
export class BookingProcessByUser {
  public BookingProcesDetails = signal<BookingDto>({} as BookingDto);

  constructor() {}
}

export interface BookingProcessRequest {
  DealerId: string;
  VehicleId: string;
  UserId: string;
}
