using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace md4.Models
{
    public class HomeViewModel
    {
        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }
        public List<Student> Students { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Submission> Submissions { get; set; }
    }
}