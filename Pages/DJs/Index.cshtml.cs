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
    public class IndexModel : PageModel
    {
        private readonly CTRL.Data.ApplicationDbContext _context;

        public IndexModel(CTRL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DJ> DJ { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DJ = await _context.DJs.ToListAsync();
        }
    }
}
