using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Userbooks
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<UserBook> UserBook { get;set; } = default!;

        public async Task OnGetAsync()
        {
            UserBook = await _context.UserBooks
                .Include(u => u.Book)
                .Include(u => u.User).ToListAsync();
        }
    }
}
