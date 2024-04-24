namespace StudyProject.Models
{
    /// <summary>
    /// Представляет назначение задачи.
    /// </summary>
    public class TaskAssignment
    {
        /// <summary>
        /// Идентификатор назначения задачи.
        /// </summary>
        public int AssignmentId { get; set; }

        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Идентификатор студента, связанного с назначением задачи.
        /// </summary>
        public int StudentId { get; set; }
    }
}
