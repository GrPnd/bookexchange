using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.MyBooks;

public class AddReview : PageModel
{
    private readonly DAL.AppDbContext _context;

    public AddReview(DAL.AppDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; } = default!;


    [BindProperty(SupportsGet = true)]
    public int BookId { get; set; }


    [BindProperty]
    [Required]
    [StringLength(500, ErrorMessage = "Review must be under 500 characters.")]
    public string Review { get; set; } = default!;
    

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var book = _context.UserBooks.FirstOrDefault(ub => ub.BookId == BookId && ub.User!.UserName == UserName);
        
        if (book != null)
        {
            book.Review = Review;
            _context.SaveChanges();
        }
        return RedirectToPage("./Index", new { userName = UserName });
    }
}