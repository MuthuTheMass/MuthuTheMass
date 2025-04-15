export interface PreUserBookingDetails {
    bookedDate: Date;
    bookingId: string;
    customerName: string;
    customerMobileNumber: string;
    customerEmailId: string;
    dealerName: string;
    dealerStorename: string;
    dealerEmail:string,
    dealerMobileNumber: string;
    dealerAddress: string;
    vehicleNumber: string;
    vehicleModal: string;
    vehicleDriverName?: string;
    vehicleDriverMobileNumber?: string;
    vehicleImage: string;
    qrCode: string;
    bookingStatus: string;
    bookingSource: string;
    advanceAmount: string;
    allotedSlots: string;
  }
  