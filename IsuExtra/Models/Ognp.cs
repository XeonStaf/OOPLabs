using System;
using System.Collections.Generic;
using System.Linq;
using Isu;

namespace IsuExtra.Models
{
    public class Ognp
    {
        private List<ExtraGroup> _streams;

        public Ognp(string name, char megaFaculty)
        {
            Name = name;
            MegaFaculty = megaFaculty;
            Id = Guid.NewGuid();
            _streams = new List<ExtraGroup>();
        }

        public string Name { get; }
        public char MegaFaculty { get; }
        public Guid Id { get; }

        public ExtraGroup AddStream(CourseNumber course, int maxStudent)
        {
            int groupNumber = _streams.Capacity == 0 ? 0 : _streams.Last().GroupNumber + 1;
            var newStream = new ExtraGroup(course, groupNumber, maxStudent);
            _streams.Add(newStream);
            return newStream;
        }

        public List<Student> GetStudents()
        {
            var result = _streams.SelectMany(stream => stream.Students).ToList();
            return result;
        }

        public List<ExtraGroup> GetStreams()
        {
            return _streams;
        }
    }
}