using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.MyBooks;

public class TradeDetails : PageModel
{
    private readonly DAL.AppDbContext _context;

    public TradeDetails(DAL.AppDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public int BookId { get; set; }
    
    
    [BindProperty(SupportsGet = true)]
    public string? UserName { get; set; }

    public Trade? Trade { get; set; }

    
    public void OnGet()
    {
        Trade = _context.Trades
            .Include(t => t.BookA).ThenInclude(ub => ub!.Book)
            .Include(t => t.BookB).ThenInclude(ub => ub!.Book)
            .FirstOrDefault(t => t.BookBId == BookId);
    }

    public IActionResult OnPost(string action)
    {
        Trade = _context.Trades
            .Include(t => t.BookA)
            .Include(t => t.BookB)
            .FirstOrDefault(t => t.BookBId == BookId);

        if (Trade == null)
        {
            return NotFound();
        }
        

        if (action == "accept")
        {
            Trade.TradeStatus = ETradeStatus.Done;
            
            Trade.BookAStatus = ETradeStatus.Accepted;
            Trade.BookBStatus = ETradeStatus.Accepted;

            
            var userAId = Trade.BookA!.UserId;
            var userBId = Trade.BookB!.UserId;
            
            var oldBookAOwner = _context.UserBooks
                .FirstOrDefault(ub => ub.BookId == Trade.BookAId && ub.UserId == userAId);
            if (oldBookAOwner != null)
            {
                oldBookAOwner.IsOwner = false;
            }

            var oldBookBOwner = _context.UserBooks
                .FirstOrDefault(ub => ub.BookId == Trade.BookBId && ub.UserId == userBId);
            if (oldBookBOwner != null)
            {
                oldBookBOwner.IsOwner = false;
            }
            
            _context.UserBooks.Add(new UserBook
            {
                UserId = userAId,
                BookId = Trade.BookBId,
                IsOwner = true
            });

            _context.UserBooks.Add(new UserBook
            {
                UserId = userBId,
                BookId = Trade.BookAId,
                IsOwner = true
            });
            
            _context.SaveChanges();

            return RedirectToPage("./Index", new { userName = UserName });
        } 
        
        if (action == "reject")
        {
            
            Trade.TradeStatus = ETradeStatus.Rejected;
            
            Trade.BookAStatus = ETradeStatus.Rejected;
            Trade.BookBStatus = ETradeStatus.Rejected;

            _context.SaveChanges();
            return RedirectToPage("./Index", new { userName = UserName });
        }

        return RedirectToPage(new { BookId });
    }
}