using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyProject.Models
{

    /// <summary>
    /// Представляет участника проекта.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Идентификатор студента.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int StudentId { get; set; }

        /// <summary>
        /// Идентификатор проекта.
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public int ProjectId { get; set; }

        
    }
}
