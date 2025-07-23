namespace gofundraise3.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public List<TaskItem> Tasks { get; set; } = new();
    }

    public enum ProjectStatus
    {
        Planning,
        Active,
        OnHold,
        Completed,
        Cancelled
    }
}
