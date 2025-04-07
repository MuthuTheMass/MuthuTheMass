export interface VehicleModal {
  vehicleId: string;
  vehicleName: string;
  vehicleNumber: string;
  vehicleImage: string;
  driverName: string;
  driverPhoneNumber: string;
  vehicleModel: string;
  alternative_Phone_Number: string;
}

export interface miniVehicleModal {
  vehicleId: string;
  vehicleName: string;
  vehicleNumber: string;
}

export interface VehicleDetailOfSingle {
  vehicleId: string;
  vehicleName: string;
  vehicleNumber: string;
  vehicleModel?: string;
}
