namespace DataAccessLayer.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime AddedTime { get; set; }
    public DateTime ExpiryTime { get; set; }
}