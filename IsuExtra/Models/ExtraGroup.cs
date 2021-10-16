using System.Collections.Generic;
using System.Linq;
using Isu;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class ExtraGroup : Group
    {
        public ExtraGroup(CourseNumber course, int groupNumber, int maxStudent)
            : base(course, groupNumber)
        {
            MaxStudent = maxStudent;
            Classes = new List<Lesson>();
        }

        public List<Lesson> Classes { get; }
        public int MaxStudent { get; }

        public Lesson AddClass(string teacher, string room, int day, int startHour, int endHour, int startMinute, int endMinute)
        {
            var newClass = new Lesson(room, teacher, day, startHour, endHour, startMinute, endMinute);
            if (Classes.Any(thisClass => newClass.CheckOverlap(thisClass)))
            {
                throw new IsuExtraException("You can't add class which overlap with existing class");
            }

            Classes.Add(newClass);
            return newClass;
        }

        public bool CheckStreamsOverlap(ExtraGroup other)
        {
            return Classes.Any(thisLesson => other.Classes.Any(otherLesson => thisLesson.CheckOverlap(otherLesson)));
        }

        public List<Student> GetStudents()
        {
            return this.Students;
        }
    }
}