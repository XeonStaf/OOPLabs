using System;
using System.Collections.Generic;
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
            if (name[0] != 'M' && name[1] != '3')
                throw new IsuException("Group is incorrect");
            int courseNum = name[2] - '0';
            if (courseNum > 9 || courseNum < 1)
                throw new IsuException("That course can't be exists");
            int groupNum = ((name[3] - '0') * 10) + (name[4] - '0');
            if (groupNum > 99 || groupNum < 0)
                throw new IsuException("That group can't be exists");
            CourseNumber targetCourse = null;
            foreach (CourseNumber course in _courses)
            {
                if (course.CourseNum == courseNum)
                {
                    targetCourse = course;
                }
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

        public Student GetStudent(int id)
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
            foreach (CourseNumber course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student student in group.Students)
                    {
                        if (student.Name == name)
                            return student;
                    }
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            Group target = FindGroup(groupName);
            return target.Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var result = new List<Student>();
            foreach (CourseNumber course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    foreach (Student student in group.Students)
                    {
                        result.Add(student);
                    }
                }
            }

            return result;
        }

        public Group FindGroup(string groupName)
        {
            foreach (CourseNumber course in _courses)
            {
                foreach (Group group in course.Groups)
                {
                    if (group.Str() == groupName)
                        return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            foreach (CourseNumber course in _courses)
            {
                if (course.Equals(courseNumber))
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
}
}