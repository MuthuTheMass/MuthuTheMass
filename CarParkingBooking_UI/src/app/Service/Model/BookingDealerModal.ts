export interface BookingDto {
  dealerId?: string;
  bookingId?: string;
  dealerEmail?: string;
  customerId?: string;
  customerDetails: CustomerDetails;
  vehicleInfo?: VehicleInformation;
  bookingSource: BookingSources;
  bookingDate: CarBookingDates;
  generatedQrCode?: Uint8Array;
  advanceAmount?: string;
  bookingStatus: Status;
  allottedSlot?: string;
}

export interface CustomerDetails {
  customerName: string;
  email?: string;
  mobileNumber: string;
  address: string;
  proof?: Proof;
}

export interface VehicleInformation {
  vehicleId: string;
  vehicleNumber: string;
  vehicleModel: string;
  vehicleImage?: string;
}

export interface CarBookingDates {
  userBookingDate?: Date;
  from?: Date;
  to?: Date;
}

export interface Status {
  state: BookingProcessDetails;
  reason?: string;
}

export enum BookingProcessDetails {
  Unknown = -1, // Unknown
  InProgress = 0, // Slot in progress
  SlotConfirmed = 1, // Booking slot confirmed
  VehicleEntered = 2, // Vehicle entered the parking area
  VehicleExited = 3, // Vehicle exited the parking area
}

export enum BookingSources {
  Dealer = 0,
  User = 1,
}

export interface Proof {
  Type: string;
  Number: string;
}

export interface CarBookingDetailDto {
  bookingId?: string;
  customerId?: string;
  customerName?: string;
  customerEmail?: string;
  customerPhoneNumber?: string;
  vehicleNumber?: string;
  vehicleModel?: string;
  vehicleImage?: string;
  bookingSource?: string;
  bookingFromDate?: Date;
  bookingToDate?: Date;
  qrCode?: string;
  advanceAmount?: string;
  bookingStatus?: string;
  allottedSlots?: string;
}
