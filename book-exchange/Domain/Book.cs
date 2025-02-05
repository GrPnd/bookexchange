using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Book
{
    public int Id { get; set; }

    [MaxLength(128)]
    public string Title { get; set; } = default!;
    
    [MaxLength(128)]
    public string AuthorFirstName { get; set; } = default!;
    
    [MaxLength(128)]
    public string AuthorLastName { get; set; } = default!;


    [MaxLength(128)]
    public string Description { get; set; } = default!;

    public ICollection<BookGenre>? BookGenres { get; set; }
    public ICollection<UserBook>? UserBooks { get; set; }
}