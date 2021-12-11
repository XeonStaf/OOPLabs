using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reports.DAL.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public bool Closed { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;
        public Employee Employee { get; set; }
        
        private Report()
        {}

        public Report(Guid id, Employee employee)
        {
            Id = id;
            Employee = employee;
        }
    }
}