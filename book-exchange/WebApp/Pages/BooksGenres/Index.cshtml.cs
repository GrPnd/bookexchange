using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.BooksGenres
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<BookGenre> BookGenre { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BookGenre = await _context.BookGenres
                .Include(b => b.Book)
                .Include(b => b.Genre).ToListAsync();
        }
    }
}
