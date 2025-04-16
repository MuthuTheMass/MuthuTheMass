namespace CarParkingSystem.Domain.ValueObjects;

public enum BookingProcessDetails
{
    Unknown = -1, //Unknown
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

public enum BookingStatus
{
    Failed = 0,
    Pending = 1,
    Success = 2,
}

public enum modeOfPayment
{
    Cash = 0,
    UPI = 1,
    Card = 2,
    Wallet = 3,
    NetBanking = 4,
    Other = 5,
}

public enum Currency
{
    INR,
    USD,
    AED,
    EUR,
    GBP,
    JPY,
    CAD,
    AUD,
    CNY,
    CHF,
    SEK,
    NZD,
    MXN,
    SGD,
    HKD,

}