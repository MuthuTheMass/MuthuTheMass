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


export interface UserDetailsForDealer{
  name:string
  picture:File
  mobileNumber:string
}

