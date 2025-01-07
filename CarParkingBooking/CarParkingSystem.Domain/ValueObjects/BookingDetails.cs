namespace CarParkingSystem.Domain.ValueObjects;

public enum BookingProcessDetails
{
    InProgress = 0, //Slot in progress
    SlotConfirmed = 1, //booking slot confirmed
    VehicleEntered = 2, // vehicle get entered in the parking area
    VehicleExited = 3, // vehicle get out from the parking area
}

public enum BookingSources
{
    Dealer = 0,
    User = 1,
}