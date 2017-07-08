using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using RazorPagesCommands.Commands;
using RazorPagesCommands.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPagesCommands.Pages.People
{
    public class IndexModel : CommandPageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<Person> People { get; private set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        public async Task OnGetAsync()
        {
            People = await _db.People.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnDeletePerson(int id)
        {
            var person = await _db.People.FindAsync(id);

            if (person != null)
            {
                _db.People.Remove(person);
                await _db.SaveChangesAsync();
            }

            Message = $"Person with id '{id}' deleted successfully";

            return RedirectToPage();
        }
    }
}
