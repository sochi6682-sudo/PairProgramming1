namespace StubServer.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = "";
    public int Value { get; set; }
    public int Amount { get; set; }
    public string? ErrorCode { get; set; }
}
