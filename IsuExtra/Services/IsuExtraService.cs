using System;
using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Services;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<OGNP> _ognps = new List<OGNP>();
        public IsuService IsuService { get; } = new IsuService();

        public ExtraGroup AddGroup(string name, int maxStudent)
        {
            Group buffer = IsuService.AddGroup(name);
            var newGroup = new ExtraGroup(buffer.CourseNumber, buffer.GroupNumber, maxStudent);
            CourseNumber course = buffer.CourseNumber;
            course.Groups.Remove(buffer);
            course.Groups.Add(newGroup);
            return newGroup;
        }

        public ExtraClassStudent AddStudent(ExtraGroup group, string name)
        {
            Student buffer = IsuService.AddStudent(group, name);
            var newStudent = new ExtraClassStudent(group, name);
            group.Students.Remove(buffer);
            group.Students.Add(newStudent);
            return newStudent;
        }

        public OGNP AddOGNP(string name, char megaFaculty)
        {
            var newOGNP = new OGNP(name, megaFaculty);
            _ognps.Add(newOGNP);
            return newOGNP;
        }

        public OGNP GetOGNP(ExtraGroup flow)
        {
            foreach (var ognp in _ognps.Where(ognp => ognp.GetFlows().Contains(flow)))
            {
                return ognp;
            }

            throw new IsuExtraException("This flow is not contains in any OGNP");
        }

        public void EnrollStudentToFlow(ExtraGroup flow, ExtraClassStudent student)
        {
            OGNP ognp = GetOGNP(flow);
            if (student.Group.GroupHumanString()[0] == ognp.MegaFaculty)
                throw new IsuExtraException("You can't enroll on OGNP, which same as yours");
            if (flow.Students.Count > flow.MaxStudent)
                throw new IsuExtraException("This flow is full");
            if (flow.CheckFlowsOverlap((ExtraGroup)student.Group))
                throw new IsuExtraException("Found intersection!");
            if (student.AdditionalClass.Any(item => flow.CheckFlowsOverlap(item)))
            {
                throw new IsuExtraException("Found intersection!");
            }

            flow.Students.Add(student);
            student.AdditionalClass.Add(flow);
        }

        public void RemoveStudentFromFlow(ExtraGroup flow, ExtraClassStudent student)
        {
            if (!student.AdditionalClass.Contains(flow) || !flow.Students.Contains(student))
                throw new IsuExtraException("Student aren't enrolled on this flow");
            flow.Students.Remove(student);
            student.AdditionalClass.Remove(flow);
        }

        public List<ExtraGroup> GetFlows(OGNP ognp)
        {
            return ognp.GetFlows();
        }

        public List<Student> GetStudents(ExtraGroup flow)
        {
            return flow.GetStudents();
        }

        public List<Student> GetNotEnrolledStudents(ExtraGroup group)
        {
            return (from student in @group.GetStudents()
                let extraStudent = (ExtraClassStudent)student
                where extraStudent.AdditionalClass.Count == 0
                select student).ToList();
        }
    }
}