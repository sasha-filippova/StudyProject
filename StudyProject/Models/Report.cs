namespace StudyProject.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int ProjectId { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportText { get; set; }
        //public Project Project { get; set; }
    }
}
