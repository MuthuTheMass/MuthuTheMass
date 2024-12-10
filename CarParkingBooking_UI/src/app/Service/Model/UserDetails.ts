export interface userDetails{
    userID:string,
    name: string,
    profilePicture: string,
    email: string,
    mobileNumber: string,
    address: string
}

export interface UserUpdateData {
  Address: string,
  Email: string,
  MobileNumber: string,
  Name: string,
  ProfilePicture: File
}

export interface userDetailsForDealer{
  name:string
  email:string
  mobileNumber:string
}
