using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.MyBooks;

public class GiveRating : PageModel
{
    private readonly DAL.AppDbContext _context;

    public GiveRating(DAL.AppDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; } = default!;


    [BindProperty(SupportsGet = true)]
    public int BookId { get; set; }


    [BindProperty]
    public int Rating { get; set; } = default!;
    

    public IActionResult OnPost()
    {
        var book = _context.UserBooks.FirstOrDefault(ub => ub.BookId == BookId && ub.User!.UserName == UserName);
        
        if (book != null)
        {
            book.Rating = Rating;
            _context.SaveChanges();
        }
        return RedirectToPage("./Index", new { userName = UserName });
    }
}