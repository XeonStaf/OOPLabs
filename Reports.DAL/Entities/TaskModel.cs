using System;
using System.Threading.Tasks;

namespace Reports.DAL.Entities
{
    public class TaskModel
    {
        private TaskModel()
        {
        }

        public TaskModel(Guid id, TaskState taskState, string content, Employee assignedEmployee, DateTime created,
            DateTime lastChanges)
        {
            Id = id;
            State = taskState;
            Content = content;
            AssignedEmployee = assignedEmployee;
            Created = created;
            LastChanges = lastChanges;
        }

        public Guid Id { get; set; } = new Guid();
        public TaskState State { set; get; }
        public string Content { set; get; }
        public Employee AssignedEmployee { set; get; }
        public DateTime Created { set; get; } = DateTime.Now;
        public DateTime LastChanges { set; get; } = DateTime.Now;
    }
}