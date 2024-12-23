using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace md4.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public float Score { get; set; }

        // Navigation properties
        [ValidateNever]

        public Assignment Assignment { get; set; }
        [ValidateNever]

        public Student Student { get; set; }


    }
}
