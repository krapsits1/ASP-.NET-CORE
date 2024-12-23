using System.ComponentModel.DataAnnotations;

namespace md4.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }

        // Navigation properties
        public Teacher Teacher { get; set; }
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }

}
