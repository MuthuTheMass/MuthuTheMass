import {miniVehicleModal} from "./VehicleModal";

export interface userDetails{
    userID:string,
    name: string,
    profilePicture: string,
    email: string,
    mobileNumber: string,
    address: string
    carDetails:miniVehicleModal[]
}

export interface UserUpdateData {
  Address: string,
  Email: string,
  MobileNumber: string,
  Name: string,
  ProfilePicture: File
}


export interface DashboardDetailsForDealer {
  newCustomers?: UserDetailsForDealer[];
  availableSlots: number;
  bookedSlots: number;
  totalSlots: number;
}

export interface UserDetailsForDealer {
  name?: string;
  picture?: File | string | any;
  mobileNumber?: string;
}

