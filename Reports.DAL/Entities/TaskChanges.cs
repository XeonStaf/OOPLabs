using System;

namespace Reports.DAL.Entities
{
    public class TaskChanges
    {
        public Guid Id { get; set; }
        public TaskModel ChangedTask { get; set; }
        public TaskState OldState { set; get; }
        public string OldContent { set; get; }
        public Guid OldAssignedEmployeeId { set; get; }
        public TaskState NewState { set; get; }
        public string NewContent { set; get; }
        public Guid NewAssignedEmployeeId { set; get; }
        public Guid ChangedPersonId { set; get; }

        private TaskChanges()
        {
        }

        public TaskChanges(Guid id, TaskModel task, TaskState oldState, string oldContent, Guid oldAssignedEmployeeId,
            TaskState newState, string newContent, Guid newAssignedEmployeeId, Guid changedPersonId)
        {
            Id = id;
            ChangedTask = task;
            OldState = oldState;
            OldContent = oldContent;
            OldAssignedEmployeeId = oldAssignedEmployeeId;
            NewState = newState;
            if (newState == default)
                NewState = oldState;
            NewContent = newContent;
            if (newContent == null)
                NewContent = oldContent;
            NewAssignedEmployeeId = newAssignedEmployeeId;
            if (NewAssignedEmployeeId == default)
                NewAssignedEmployeeId = oldAssignedEmployeeId;
            ChangedPersonId = changedPersonId;
        }
    }
}