using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GroupApp.Data.Configurations
{
    public class TaskRelEntityConfiguration : IEntityTypeConfiguration<TaskRelEntity>
    {
        public void Configure(EntityTypeBuilder<TaskRelEntity> builder)
        {
            builder.ToTable("TaskAssignments"); // Daha açıklayıcı bir tablo ismi

            builder.HasKey(tr => new { tr.TaskId, tr.UserId, tr.Id }); // Composite Primary Key

            // Task ile ilişki (1-N)
            builder.HasOne(tr => tr.Task)
                .WithMany(t => t.AssignedUsers) // Task, birçok kullanıcıya atanabilir
                .HasForeignKey(tr => tr.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ile ilişki (1-N)
            builder.HasOne(tr => tr.User)
                .WithMany(u => u.AssignedTasks) // Kullanıcı, birçok göreve atanabilir
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
