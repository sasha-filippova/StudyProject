namespace StudyProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }

        //public Task Task { get; set; }
        //public User User { get; set; }

    }
}
