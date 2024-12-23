using System.ComponentModel.DataAnnotations;

namespace md4.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Gender { get; set; }

        public string StudentIdNumber { get; set; }
        public string FullName => $"{Name} {Surname}";

        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    }
}
