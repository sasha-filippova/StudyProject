namespace StudyProject.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int StatusId { get; set; }

        //public Status Status { get; set; }
        //public Task Task { get; set; }
        //public Report Report { get; set; }
        //public Member Member { get; set; }
    }
}
