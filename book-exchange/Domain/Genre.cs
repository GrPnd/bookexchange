using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Genre
{
    public int Id { get; set; }

    [MaxLength(128)]
    public string Name { get; set; } = default!;

    public ICollection<BookGenre>? BookGenres { get; set; }
}