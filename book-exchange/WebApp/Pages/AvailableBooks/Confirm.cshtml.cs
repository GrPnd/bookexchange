using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.AvailableBooks;

public class Confirm : PageModel
{
    
    private readonly DAL.AppDbContext _context;

    public Confirm(DAL.AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty(SupportsGet = true)]
    public string? UserName { get; set; }

    [BindProperty(SupportsGet = true)]
    public int BookId { get; set; }
    
    [BindProperty]
    public string? Action { get; set; }
    
    public Book? Book { get; set; }
    
    public void OnGet()
    {
        Book = _context.Books.FirstOrDefault(b => b.Id == BookId);
    }

    public IActionResult OnPost()
    {
        if (Action == "yes")
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == UserName);
            Book = _context.Books.FirstOrDefault(b => b.Id == BookId);
            if (user != null && Book != null)
            {
                var userBook = new UserBook
                {
                    UserId = user.Id,
                    BookId = Book.Id,
                    IsOwner = true,
                    WantTrade = false
                };
                _context.UserBooks.Add(userBook);
                _context.SaveChanges();
                return RedirectToPage("../MyBooks/Index", new { userName = UserName });
            }
        }

        return RedirectToPage("./Index", new { userName = UserName });
    }
    
    
}