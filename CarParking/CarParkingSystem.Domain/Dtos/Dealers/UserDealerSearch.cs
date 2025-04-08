namespace CarParkingSystem.Domain.Dtos.Dealers;

public record UserDealerSearch
(
    string DealerId,
    string DealerStoreName,
    string DealerAddress,
    string Price,
    string StoreImage);