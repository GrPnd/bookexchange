using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.TradeBook
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }
        
        [BindProperty(SupportsGet = true)]
        public string? Username { get; set; }

        public IList<Book> BookList { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == Username);
            if (user == null)
            {
                BookList = [];
            }
            else
            {
                BookList = await _context.Books
                    .Where(b => b.UserBooks != null && b.UserBooks.Any(ub => ub.UserId != user!.Id))
                    .ToListAsync();      
            }
          

        }


    }
}