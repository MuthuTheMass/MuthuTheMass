export interface CarBookingDetailDto {
    bookingId?: string;
    customerId?: string;
    customerName?: string;
    customerEmail?: string;
    customerPhoneNumber?: string;
    vehicleNumber?: string;
    vehicleModel?: string;
    bookingSource?: string;
    bookingFromDate?: Date;
    bookingToDate?: Date;
    qrCode?: string;
    advanceAmount?: string;
    bookingStatus?: string;
    allottedSlots?: string;
  }