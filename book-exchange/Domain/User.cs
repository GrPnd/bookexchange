using System.ComponentModel.DataAnnotations;

namespace Domain;

public class User
{
    public int Id { get; set; }
    
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    [MaxLength(128)]
    public string UserName { get; set; } = default!;

    public ICollection<UserBook>? UserBooks { get; set; }
}