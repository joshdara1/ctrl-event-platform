using System.Threading.Tasks;
using CTRL.Data;
using CTRL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CTRL.Pages.Events
{
    [Authorize]
    public class TicketModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; } = default!;
        public IdentityUser CurrentUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var registrationExists = await _context.EventRegistrations
                .AnyAsync(r => r.EventId == id && r.UserId == user.Id);

            if (!registrationExists)
            {
                return RedirectToPage("/Events/Details", new { id = id });
            }

            var eventItem = await _context.Events
                .Include(e => e.DJ)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            Event = eventItem;
            CurrentUser = user;

            return Page();
        }
    }
}