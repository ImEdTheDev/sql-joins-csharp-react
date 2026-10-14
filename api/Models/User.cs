namespace PurchasesApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    // Navigation property: one user has zero, one, or many purchases.
    public List<Purchase> Purchases { get; set; } = [];
}
