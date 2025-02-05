using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class UserBook
{
    public int Id { get; set; }
    public string? Review { get; set; }
    public int? Rating { get; set; }
    public bool IsOwner { get; set; }
    public bool WantTrade { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public int BookId { get; set; }
    public Book? Book { get; set; }
    
    
    [InverseProperty("BookA")]
    public ICollection<Trade>? TradesAsBookA { get; set; }

    [InverseProperty("BookB")]
    public ICollection<Trade>? TradesAsBookB { get; set; }
}