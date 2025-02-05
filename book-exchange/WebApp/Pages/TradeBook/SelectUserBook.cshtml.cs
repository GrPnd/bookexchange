using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.TradeBook;

public class SelectUserBook : PageModel
{
    private readonly DAL.AppDbContext _context;

    public SelectUserBook(DAL.AppDbContext context)
    {
        _context = context;
    }

    
    [BindProperty(SupportsGet = true)]
    public string? UserName { get; set; }

    [BindProperty(SupportsGet = true)]
    public int BookIdToTrade { get; set; }
    
    [BindProperty]
    public int BookIdExchange { get; set; }
    
    public IList<UserBook> UserBook { get; set; } = [];

    public void OnGet()
    {
        UserBook = _context.UserBooks
            .Include(ub => ub.User)  // Include the User navigation property
            .Include(ub => ub.Book)  // Include the Book navigation property
            .Where(ub => ub.User!.UserName == UserName)
            .ToList();
    }

    public void OnPost()
    {
        var bookA = _context.UserBooks.FirstOrDefault(b => b.BookId == BookIdExchange);
        var bookB = _context.UserBooks.FirstOrDefault(b => b.BookId == BookIdToTrade);
        
        var trade = new Trade()
        {
            BookA = bookA,
            BookAStatus = ETradeStatus.Requested,
            BookB = bookB,
            BookBStatus = ETradeStatus.Requested,
            TradeStatus = ETradeStatus.Requested
        };
        _context.Trades.Add(trade);
        _context.SaveChanges();
    }
    
    
}