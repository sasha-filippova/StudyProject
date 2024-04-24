namespace StudyProject.Models
{
    /// <summary>
    /// Представляет комментарий.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, оставившего комментарий.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор задачи, к которой относится комментарий.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата публикации комментария.
        /// </summary>
        public DateTime DatePosted { get; set; }
        
    }
}
