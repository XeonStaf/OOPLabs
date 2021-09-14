using System.Collections.Generic;

namespace Isu
{
    public class CourseNumber
    {
        public CourseNumber(int num)
        {
            CourseNum = num;
            Groups = new List<Group>();
        }

        public int CourseNum { get;  }
        public List<Group> Groups { get; }
    }
}