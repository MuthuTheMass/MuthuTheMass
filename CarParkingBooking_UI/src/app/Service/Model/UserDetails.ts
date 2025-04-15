import { miniVehicleModal } from './VehicleModal';
import { BookingProcessDetails } from './BookingDealerModal';

export interface userDetails {
  userID: string;
  name: string;
  profilePicture: string;
  email: string;
  mobileNumber: string;
  address: string;
  carDetails: miniVehicleModal[];
}

export interface UserUpdateData {
  Address: string;
  Email: string;
  MobileNumber: string;
  Name: string;
  ProfilePicture: File;
}

export interface DashboardDetailsForDealer {
  newCustomers?: UserDetailsForDealer[];
  availableSlots: number;
  bookedSlots: number;
  totalSlots: number;
  recentBookings?: RecentBookingInDealerDashBoard[];
}

export interface UserDetailsForDealer {
  name?: string;
  picture?: File | string | any;
  mobileNumber?: string;
}

export interface RecentBookingInDealerDashBoard {
  bookingID: string;
  vehicleNumber: string;
  date: Date | null;
  status: string;
  slot_Number: string;
  qRCode: string;
}

export interface BookingInUserDashBoard {
  bookingId: string;
  vehicleNumber: string;
  bookedDate: Date | null;
  status: BookingProcessDetails;
  slotNumber: string;
  parkingName: string;
}
