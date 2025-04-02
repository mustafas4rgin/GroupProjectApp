namespace GroupApp.Data;

public class UserEntity : EntityBase
{
    public int RoleId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePicture { get; set; }
    public RoleEntity Role { get; set; } = null!;
    public ICollection<TaskRelEntity> AssignedTasks { get; set; } = new List<TaskRelEntity>();


}
