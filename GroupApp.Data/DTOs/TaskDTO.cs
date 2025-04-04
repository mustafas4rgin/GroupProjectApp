﻿namespace GroupApp.Data;

public class TaskDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public int CreatedByUserId { get; set; }
    public string? Notifications { get; set; } 
}
