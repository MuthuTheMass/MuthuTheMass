export interface dealerVM {
  dealerId: string;
  dealerName: string;
  dealerEmail: string;
  dealerPhoneNo: string;
  dealerDescription: string;
  dealerTiming: {
    monday: DealerTiming;
    tuesday: DealerTiming;
    wednesday: DealerTiming;
    thursday: DealerTiming;
    friday: DealerTiming;
    saturday: DealerTiming;
    sunday: DealerTiming;
    alwaysAvailable: string;
  };
  dealerAddress: string;
  dealerLandmark: string;
  dealerCity: string;
  dealerState: string;
  dealerCountry: string;
  dealerStoreName: string;
  dealerLocationURL: string;
  dealerOpenOrClosed: boolean;
  dealerRating: string;
  oneHourAmount: string;
  image: string;
}

export interface DealerTiming {
  start: string;
  stop: string;
}

export interface dealerData {
  access: string;
  accessToken: string;
  email: string;
  id: string | null;
  userName: string;
}

export interface UserDealerSearch {
  dealerId: string;
  dealerStoreName: string;
  dealerAddress: string;
  price: string;
  storeImage: string;
}
