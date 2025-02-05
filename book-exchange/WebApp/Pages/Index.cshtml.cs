using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace WebApp.Pages;

public class Index : PageModel
{
    private readonly DAL.AppDbContext _context;

    public Index(DAL.AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public string Username { get; set; } = default!;

    public string ValidationMessage { get; private set; } = default!;

    public void OnGet()
    {
        ValidationMessage = string.Empty;
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(Username))
        {
            ValidationMessage = "Username cannot be empty.";
        }
        else if (Username.Length < 1 || Username.Length > 15)
        {
            ValidationMessage = "Username must be between 1 and 15 characters long.";
        }
        else if (!Regex.IsMatch(Username, @"^[a-zA-Z0-9]+$"))
        {
            ValidationMessage = "Username can only contain letters and numbers.";
        }
        else if (!_context.Users.Any(u => u.UserName == Username) && Username.ToLower() != "admin")
        {
            ValidationMessage = "Username doesnt exist.";
        }
        else
        {
            return RedirectToPage("/MyBooks/Index", new { username = Username });
        }

        return Page();
    }
}