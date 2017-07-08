using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorPagesCommands.Commands;
using RazorPagesCommands.Data;
using System.Threading.Tasks;

namespace RazorPagesCommands.Pages.People
{
    public class CreateModel : CommandPageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Person Person { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnCreatePerson()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.People.Add(Person);
            await _db.SaveChangesAsync();

            Message = "New person created successfully!";

            return RedirectToPage("./Index");
        }
    }
}
