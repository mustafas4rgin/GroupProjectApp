using AutoMapper;
using GroupApp.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDTO, UserEntity >().ReverseMap();
        CreateMap<TaskDTO, TaskEntity>().ReverseMap();
        CreateMap<AssignedTaskDTO, TaskRelEntity>().ReverseMap();
    }
}