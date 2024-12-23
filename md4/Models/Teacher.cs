using System.ComponentModel.DataAnnotations;

namespace md4.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
                
        public string Gender { get; set; }  
        public DateTime ContractDate { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
