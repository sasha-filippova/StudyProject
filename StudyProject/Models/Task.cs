namespace StudyProject.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }
        //public Project Project { get; set; }
        //public Status Status { get; set; }
        //public Category Category { get; set; }
        //public TaskAssignment TaskAssignment { get; set; }
        //public Comment Comment { get; set; }
    }
}
