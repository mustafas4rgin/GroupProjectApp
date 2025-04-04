namespace GroupApp.Data;

    using GroupApp.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Description).HasMaxLength(1000);
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.DueDate).IsRequired();
            builder.Property(t => t.Notifications).IsRequired();
            builder.Property(t => t.IsCompleted).IsRequired();

            builder.HasMany(t => t.AssignedUsers)
                .WithOne(tr => tr.Task) // Güncellendi: TaskRels -> AssignedUsers
                .HasForeignKey(tr => tr.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
