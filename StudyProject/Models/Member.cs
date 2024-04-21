using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyProject.Models
{

    public class Member
    {
        [Key]
        [Column(Order = 0)]
        public int StudentId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ProjectId { get; set; }

        //public Student Student { get; set; }
        //public Project Project { get; set; }
    }
}
