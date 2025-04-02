namespace GroupApp.Data;

    public class TaskRelEntity : EntityBase
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public virtual TaskEntity Task { get; set; }
        public virtual UserEntity User { get; set; }
    }
