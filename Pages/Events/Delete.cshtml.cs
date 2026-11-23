using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CTRL.Data;
using CTRL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CTRL.Pages.Events
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundEvent = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (foundEvent == null)
            {
                return NotFound();
            }

            Event = foundEvent;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundEvent = await _context.Events.FindAsync(id);

            if (foundEvent != null)
            {
                Event = foundEvent;
                _context.Events.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}