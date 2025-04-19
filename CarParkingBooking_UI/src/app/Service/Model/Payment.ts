export interface UserPayment {
    paymentId?: string;
    bookingId?: string;
    customerEmail?: string;
    currencyMode?: Currency | null;       // You need to define the Currency enum/type
    paymentStatus?: BookingStatus | null; // You need to define the BookingStatus enum/type
    paymentMethod?: ModeOfPayment | null; // You need to define the ModeOfPayment enum/type
    amount?: string | null;
    createdDate?: Date;
    updatedDate?: Date;
    isDeleted: boolean;
  }
  
  export enum Currency {
    INR = 'INR',
    USD = 'USD',
    EUR = 'EUR'
  }
  
  export enum BookingStatus {
    Pending = 'Pending',
    Paid = 'Paid',
    Failed = 'Failed'
  }
  
  export enum ModeOfPayment {
    CreditCard = 'CreditCard',
    UPI = 'UPI',
    Cash = 'Cash',
    BankTransfer = 'BankTransfer'
  }
  