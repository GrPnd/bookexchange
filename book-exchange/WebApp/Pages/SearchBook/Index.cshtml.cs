using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;

    [BindProperty]
    public string Search { get; set; } = default!;
    
    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; } = default!;


    public List<Book> Books { get; set; } = [];
    
    public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost(string action)
    {
        Console.WriteLine(action);
        _logger.LogInformation(Search);
        Search = Search.Trim().ToLower();
        Books = _context.Books
            .Include(g => g.BookGenres!)
            .ThenInclude(j => j.Genre)
            .Include(b => b.UserBooks)
            .Where(r => 
                r.Title.ToLower().Contains(Search) || 
                r.AuthorFirstName.ToLower().Contains(Search) ||
                r.AuthorLastName.ToLower().Contains(Search) ||
                r.Description.ToLower().Contains(Search) ||
                r.BookGenres!.Any(i => i.Genre!.Name.ToLower().Contains(Search))
            )
            .ToList();
        return Page();
    }

}