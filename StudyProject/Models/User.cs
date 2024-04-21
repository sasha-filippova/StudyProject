using System.Data;

namespace StudyProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        //public Role Role { get; set; }
        public int StudentId { get; set; }

        //public Student Student { get; set; }
        //public Comment Comment { get; set; }
    }
}
