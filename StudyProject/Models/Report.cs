namespace StudyProject.Models
{
    /// <summary>
    /// Представляет отчет.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Идентификатор отчета.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор проекта, к которому относится отчет.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Дата создания отчета.
        /// </summary>
        public DateTime ReportDate { get; set; }

        /// <summary>
        /// Текст отчета.
        /// </summary>
        public string ReportText { get; set; }
    }
}
