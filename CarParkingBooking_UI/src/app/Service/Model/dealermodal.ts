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

