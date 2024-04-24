namespace StudyProject.Models
{
    /// <summary>
    /// Представляет студента.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Идентификатор студента.
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Имя студента.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия студента.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Электронная почта студента.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Название группы студента.
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Другая информация о студенте.
        /// </summary>
        public string OtherPersonalData { get; set; }
        
        
    }
}
