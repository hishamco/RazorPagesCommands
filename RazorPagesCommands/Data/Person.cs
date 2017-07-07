using System.ComponentModel.DataAnnotations;

namespace RazorPagesCommands.Data
{
    public class Person
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}
