using System.Data;

namespace StudyProject.Models
{
    /// <summary>
    /// Представляет пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Идентификатор роли пользователя.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Идентификатор студента, связанного с пользователем.
        /// </summary>
        public int StudentId { get; set; }
    }
}
