﻿namespace GroupApp.Data;

public class RoleEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Permissions { get; set; } // JSON string of permissions
    
}
