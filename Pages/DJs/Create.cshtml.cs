using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CTRL.Data;
using CTRL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CTRL.Pages.DJs
{
    
[Authorize]
public class CreateModel : PageModel
    {
        private readonly CTRL.Data.ApplicationDbContext _context;

        public CreateModel(CTRL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DJ DJ { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DJs.Add(DJ);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
