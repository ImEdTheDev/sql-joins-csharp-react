namespace PurchasesApi.Models;

public class Purchase
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Product { get; set; } = "";
    public decimal Amount { get; set; }

    // Navigation back to the user. It stays inside the API (not part of the DTO).
    public User? User { get; set; }
}
