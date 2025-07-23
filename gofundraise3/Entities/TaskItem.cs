namespace gofundraise3.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public DateTime? DueDate { get; set; }
    }

    public enum TaskStatus
    {
        Todo,
        InProgress,
        Done,
        Cancelled
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        Critical
    }
}
