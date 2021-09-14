using System.Collections.Generic;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        public Group(CourseNumber course, int groupNumber)
        {
            CourseNumber = course;
            GroupNumber = groupNumber;
            Students = new List<Student>();
        }

        public CourseNumber CourseNumber { get; }
        public int GroupNumber { get; }
        public List<Student> Students { get; }
        public string Str()
        {
            return $"M{this.CourseNumber.CourseNum}{this.GroupNumber}";
        }
    }
}