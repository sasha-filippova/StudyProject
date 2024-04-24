namespace StudyProject.Models
{
    /// <summary>
    /// Представляет задачу.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// Описание задачи.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Идентификатор статуса.
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// Срок выполнения.
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Идентификатор проекта.
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }
        
    }
}
