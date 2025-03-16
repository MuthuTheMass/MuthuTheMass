export interface dealerVM{
  dealerStoreName: string;
  parkingaddress: any
  dealerName: string,
  dealerEmail: string,
  dealerPhoneNo: string,
  dealerDescription: string,
  dealerStartDate: string,
  dealerTiming: string,
  dealerAddress: string,
  dealerLandmark: string,
  dealerCity:string,
  dealerGPSLocation: {
    latitude: string,
    longitude: string
  },
  dealerRating: number
}

export interface dealerData{
  access:string,
  accessToken:string,
  email:string,
  id:string | null,
  userName:string
}

export interface offlinebookingVM{
  dealerEmailId:string;
  fullName: string;
  email: string;
  mobileNumber: string;
  address: string;
  proof: string;
  proofNumber: string;
  authorityOfIssue: string;
  vehicleNumber: string;
  vehicleModel: string;
  bookingDate: string;
}
