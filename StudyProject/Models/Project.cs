namespace StudyProject.Models
{
    /// <summary>
    /// Представляет проект.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Идентификатор проекта.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Название проекта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание проекта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала проекта.
        /// </summary>
        public DateOnly StartDate { get; set; }

        /// <summary>
        /// Дата окончания проекта.
        /// </summary>
        public DateOnly EndDate { get; set; }

        /// <summary>
        /// Идентификатор статуса проекта.
        /// </summary>
        public int StatusId { get; set; }
    }
}
