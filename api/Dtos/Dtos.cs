namespace PurchasesApi.Dtos;

// The API contract: what the frontend receives (not the database entities).

public record PurchaseDto(
    int Id,
    string Product,
    decimal Amount
);

public record UserPurchasesDto(
    int Id,
    string Name,
    List<PurchaseDto> Purchases
);
