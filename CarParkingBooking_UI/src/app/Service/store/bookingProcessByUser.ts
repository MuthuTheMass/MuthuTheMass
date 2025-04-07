import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BookingProcessByUser {
  public BookingProcesDetails = signal<any>({});

  constructor() {}
}

export interface BookingProcessRequest {
  DealerId: string;
  VehicleId: string;
  UserId: string;
}
