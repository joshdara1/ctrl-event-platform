using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTRL.Data;
using CTRL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CTRL.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var eventsQuery = _context.Events
                .Include(e => e.DJ)
                .AsQueryable();

            // Search functionality
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var search = SearchTerm.ToLower();

                eventsQuery = eventsQuery.Where(e =>
                    (e.Title != null && e.Title.ToLower().Contains(search)) ||
                    (e.Venue != null && e.Venue.ToLower().Contains(search)) ||
                    (e.Category != null && e.Category.ToLower().Contains(search)) ||
                    (e.Description != null && e.Description.ToLower().Contains(search)) ||
                    (e.DJ != null && e.DJ.Name != null && e.DJ.Name.ToLower().Contains(search))
                );
            }

            // Sort newest events first
            Event = await eventsQuery
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }
    }
}