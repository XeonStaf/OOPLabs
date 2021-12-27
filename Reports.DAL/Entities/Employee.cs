using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid BossId { get; set; }

        public string Name { get; set; }

        private Employee()
        {
        }
        
        public Employee(Guid id, string name, Guid bossId)
        {
            Id = id;
            Name = name;
            BossId = bossId;
        }
    }
}