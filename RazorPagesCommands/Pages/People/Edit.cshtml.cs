using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using RazorPagesCommands.Commands;
using RazorPagesCommands.Data;
using System.Threading.Tasks;

namespace RazorPagesCommands.Pages.People
{
    public class EditModel : CommandPageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Person = await _db.People.FindAsync(id);

            if (Person == null)
            {
                Message = $"Person with id '{id}' not found!";
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnDeletePerson()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Person).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                Message = "Person updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                Message = $"Person with id '{Person.Id}' not found!";
            }

            return RedirectToPage("./Index");
        }
    }
}
