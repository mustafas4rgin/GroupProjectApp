using GroupApp.API.Helpers;
using GroupApp.Data;

namespace GroupApp.API.Helpers;

public class MappingHelper
{
    public static RoleEntity MapToRoleEntity(RoleDTO dto)
    {
        return new RoleEntity
        {
            Description = dto.Description,
            Name = dto.Name,
            Permissions = dto.Permissions
        };
    }
    public static UserEntity MapToUserEntity(UserDTO dto)
    {
        HashingHelper.CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        return new UserEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            ProfilePicture = dto.ProfilePicture,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            PhoneNumber = dto.PhoneNumber,
            RoleId = dto.RoleId
        };
    }
    public static TaskEntity MapToTaskEntity(TaskDTO dto)
    {
        return new TaskEntity
        {
            AssignedUserId = 1,
            CreatedByUserId = dto.CreatedByUserId,
            DueDate = dto.DueDate,
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            Notifications = "Default",
        };
    }
}
