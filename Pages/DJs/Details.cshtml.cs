using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CTRL.Data;
using CTRL.Models;

namespace CTRL.Pages.DJs
{
    public class DetailsModel : PageModel
    {
        private readonly CTRL.Data.ApplicationDbContext _context;

        public DetailsModel(CTRL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
