using CTRL.Data;
using CTRL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CTRL.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Event> FeaturedEvents { get; set; } = new List<Event>();

    public async Task OnGetAsync()
    {
        FeaturedEvents = await _context.Events
            .Include(e => e.DJ)
            .OrderByDescending(e => e.Date)
            .Take(3)
            .ToListAsync();
    }
}