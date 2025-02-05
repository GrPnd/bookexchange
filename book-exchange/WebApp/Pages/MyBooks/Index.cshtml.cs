using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.MyBooks;

public class IndexModel : PageModel
{
    private readonly DAL.AppDbContext _context;

    public IndexModel(DAL.AppDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string? Username { get; set; }
    
    public IList<UserBook> UserBook { get; set; } = [];
    public List<int> BookBIds { get; set; } = new();
    public ETradeStatus TradeStatus { get; set; }

    public void OnGet()
    {
        UserBook = _context.UserBooks
            .Include(ub => ub.User)
            .Include(ub => ub.Book)
            .Where(ub => ub.User!.UserName == Username && ub.IsOwner)
            .ToList();
        
        BookBIds = _context.Trades
            .Select(t => t.BookBId)
            .ToList();
        
        TradeStatus = _context.Trades
            .Where(t => BookBIds.Contains(t.BookBId))
            .Select(t => t.TradeStatus)
            .FirstOrDefault();
    }
}