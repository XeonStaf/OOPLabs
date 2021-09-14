using System;

namespace Isu
{
    public class Student
    {
        public Student(Group group, string name)
        {
            Group = group;
            Name = name;
            Id = Guid.NewGuid();
        }

        public Group Group { get; set; }
        public string Name { get; }
        public Guid Id { get; }
    }
}