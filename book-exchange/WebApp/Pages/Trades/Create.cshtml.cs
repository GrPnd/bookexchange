using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages.Trades
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BookAId"] = new SelectList(_context.UserBooks, "Id", "Review");
        ViewData["BookBId"] = new SelectList(_context.UserBooks, "Id", "Review");
            return Page();
        }

        [BindProperty]
        public Trade Trade { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Trades.Add(Trade);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
