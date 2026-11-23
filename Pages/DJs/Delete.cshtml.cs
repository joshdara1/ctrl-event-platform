using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CTRL.Data;
using CTRL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CTRL.Pages.DJs
{
    public class DeleteModel : PageModel
    {
        private readonly CTRL.Data.ApplicationDbContext _context;

        public DeleteModel(CTRL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DJ DJ { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dj = await _context.DJs.FirstOrDefaultAsync(m => m.Id == id);

            if (dj == null)
            {
                return NotFound();
            }
            else
            {
                DJ = dj;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dj = await _context.DJs.FindAsync(id);
            if (dj != null)
            {
                DJ = dj;
                _context.DJs.Remove(DJ);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
