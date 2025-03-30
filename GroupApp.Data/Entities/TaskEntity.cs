namespace GroupApp.Data;

public class TaskEntity : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public int AssignedUserId { get; set; }
    public UserEntity AssignedUser { get; set; } = null!;
    public int CreatedByUserId { get; set; }
    public UserEntity CreatedByUser { get; set; } = null!;
    public string? Notifications { get; set; } // JSON string of notification settings
}
