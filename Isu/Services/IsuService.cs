using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<CourseNumber> _courses = new List<CourseNumber>();
        private int _maxStudentsInGroup = 25;

        public Group AddGroup(string name)
        {
            CheckGroupPrefixStartM3(name);
            int courseNum = name[2] - '0';
            CheckCourseNumToCorrect(courseNum);
            int groupNum = ((name[3] - '0') * 10) + (name[4] - '0');
            CheckGroupNumberToCorrect(groupNum);
            CourseNumber targetCourse = null;
            foreach (var course in _courses.Where(course => course.CourseNum == courseNum))
            {
                targetCourse = course;
            }

            if (targetCourse == null)
            {
                targetCourse = new CourseNumber(courseNum);
                _courses.Add(targetCourse);
            }

            var newGroup = new Group(targetCourse, groupNum);
            targetCourse.Groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= _maxStudentsInGroup)
                throw new IsuException("This group is full");
            var newStudent = new Student(group, name);
            group.Students.Add(newStudent);
            return newStudent;
        }

        public Student GetStudent(Guid id)
        {
            foreach (CourseNumber course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student student in group.Students)
                    {
                        if (student.Id == id)
                            return student;
                    }
                }
            }

            throw new IsuException("Incorrect ID");
        }

        public Student FindStudent(string name)
        {
            return (from course in _courses from @group in course.Groups from student in @group.Students select student).FirstOrDefault(student => student.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            Group target = FindGroup(groupName);
            return target.Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return (from course in _courses from @group in course.Groups from student in @group.Students select student).ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _courses.SelectMany(course => course.Groups).FirstOrDefault(@group => @group.GroupHumanString() == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            foreach (var course in _courses.Where(course => course.Equals(courseNumber)))
            {
                return course.Groups;
            }

            return new List<Group>();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.Students.Count >= _maxStudentsInGroup)
                throw new IsuException("This group if full");
            Group oldGroup = student.Group;
            oldGroup.Students.Remove(student);
            student.Group = newGroup;
            newGroup.Students.Add(student);
        }

        private static void CheckGroupPrefixStartM3(string name)
        {
            if (name[0] != 'M' && name[1] != '3')
                throw new IsuException("Group is incorrect");
        }

        private static void CheckCourseNumToCorrect(int courseNum)
        {
            if (courseNum > 9 || courseNum < 1)
                throw new IsuException("That course can't be exists");
        }

        private static void CheckGroupNumberToCorrect(int groupNum)
        {
            if (groupNum > 99 || groupNum < 0)
                throw new IsuException("That group can't be exists");
        }
}
}