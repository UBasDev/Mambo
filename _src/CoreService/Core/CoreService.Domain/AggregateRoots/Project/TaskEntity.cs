using CoreService.Domain.Common;

namespace CoreService.Domain.AggregateRoots.Project
{
    public sealed class TaskEntity : BaseEntity<Guid>, ISoftDelete
    {
        public TaskEntity()
        {
            No = 0;
            TaskContent = null;
            TaskDetail = null;
            Project = null;
            ProjectId = null;
            TaskFields = new HashSet<TaskFieldEntity>();
            DeletedAt = null;
            IsActive = true;
            IsDeleted = false;
            UpdatedAt = null;
        }

        public UInt32 No { get; private set; }
        public TaskContentEntity? TaskContent { get; private set; }
        public TaskDetailEntity? TaskDetail { get; private set; }
        public ProjectEntity? Project { get; private set; }
        public Guid? ProjectId { get; private set; }
        public ICollection<TaskFieldEntity> TaskFields { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}