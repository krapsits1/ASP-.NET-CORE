using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace md4.Models
{
    public class Assignment
    {

        [Key]
        public int Id { get; set; }
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid course.")]
        public int CourseId { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        public Course Course { get; set; }
        //Attiecība
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
