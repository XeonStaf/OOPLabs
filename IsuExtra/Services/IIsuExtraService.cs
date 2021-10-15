using System.Collections.Generic;
using Isu;
using Isu.Services;
using IsuExtra.Models;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        OGNP AddOGNP(string name, char megaFaculty);
        ExtraClassStudent AddStudent(ExtraGroup @group, string name);
        public ExtraGroup AddGroup(string name, int maxStudents);
        void EnrollStudentToFlow(ExtraGroup flow, ExtraClassStudent student);
        void RemoveStudentFromFlow(ExtraGroup flow, ExtraClassStudent student);
        List<ExtraGroup> GetFlows(OGNP ognp);
        List<Student> GetStudents(ExtraGroup flow);
        List<Student> GetNotEnrolledStudents(ExtraGroup group);
    }
}