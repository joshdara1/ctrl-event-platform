using System;
using System.Threading.Tasks;
using CTRL.Data;
using CTRL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CTRL.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; } = default!;

        public bool IsRegistered { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.Events
                .Include(e => e.DJ)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            Event = eventItem;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    IsRegistered = await _context.EventRegistrations
                        .AnyAsync(r => r.EventId == Event.Id && r.UserId == user.Id);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAttendAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            var eventItem = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
            {
                return NotFound();
            }

            var alreadyRegistered = await _context.EventRegistrations
                .AnyAsync(r => r.EventId == id && r.UserId == user.Id);

            if (!alreadyRegistered)
            {
                var registration = new EventRegistration
                {
                    UserId = user.Id,
                    EventId = id,
                    RegistrationDate = DateTime.Now
                };

                _context.EventRegistrations.Add(registration);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Events/Tickets", new { id = id });
        }

        public async Task<IActionResult> OnPostUnregisterAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            var registration = await _context.EventRegistrations
                .FirstOrDefaultAsync(r =>
                    r.EventId == id &&
                    r.UserId == user.Id);

            if (registration != null)
            {
                _context.EventRegistrations.Remove(registration);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Events/Details", new { id = id });
        }
    }
}