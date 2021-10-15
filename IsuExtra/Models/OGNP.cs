using System;
using System.Collections.Generic;
using System.Linq;
using Isu;

namespace IsuExtra.Models
{
    public class OGNP
    {
        private List<ExtraGroup> _flows;

        public OGNP(string name, char megaFaculty)
        {
            Name = name;
            MegaFaculty = megaFaculty;
            Id = Guid.NewGuid();
            _flows = new List<ExtraGroup>();
        }

        public string Name { get; }
        public char MegaFaculty { get; }
        public Guid Id { get; }

        public ExtraGroup AddFlow(CourseNumber course, int maxStudent)
        {
            int groupNumber = _flows.Capacity == 0 ? 0 : _flows.Last().GroupNumber + 1;
            var newFlow = new ExtraGroup(course, groupNumber, maxStudent);
            _flows.Add(newFlow);
            return newFlow;
        }

        public List<Student> GetStudents()
        {
            var result = _flows.SelectMany(flow => flow.Students).ToList();
            return result;
        }

        public List<ExtraGroup> GetFlows()
        {
            return _flows;
        }
    }
}