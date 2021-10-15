using System;
using System.Collections.Generic;
using Isu;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;
namespace IsuExtra.Tests
{
    public class Tests
    {
        private IIsuExtraService _isuExtraService;
        private CourseNumber _course;
        private ExtraClassStudent _testStudent;
        private ExtraGroup _mainGroup;
        private OGNP _testOGNP;
        private ExtraGroup _flow1;
        private ExtraGroup _flow2;
        
        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
            _testOGNP = _isuExtraService.AddOGNP("Test", 'P');
            _course = new CourseNumber(2);
            _flow1 = _testOGNP.AddFlow(_course,20);
            _flow1.AddClass("Sidorova P.A.", "202", 0, 12, 15, 00, 00);
            _flow2 = _testOGNP.AddFlow(_course,10);
            _flow2.AddClass("Pivanova P.A.", "302", 0, 10, 12, 00, 30);
            _mainGroup = _isuExtraService.AddGroup("M3205", 20);
            _mainGroup.AddClass("Pikov L.O.", "303", 0, 8, 10, 00, 10);
            _testStudent = _isuExtraService.AddStudent(_mainGroup,"Ivan");
        }

        [Test]
        public void CheckForOverlap_FlowsAreOverlaped()
        {
            Assert.True(_flow1.CheckFlowsOverlap(_flow2));
        }

        [Test]
        public void EnrollStudentToOGNP_CathException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.EnrollStudentToFlow(_flow2, _testStudent);
            });
        }
        
        [Test]
        public void EnrollStudentToOGNP_ListContainsObject()
        {
            _isuExtraService.EnrollStudentToFlow(_flow1, _testStudent);
            Assert.Contains(_testStudent, _flow1.Students);
            Assert.Contains(_flow1, _testStudent.AdditionalClass);
        }

        [Test]
        public void RemoveStudentFromFlow_FlowNotContainStudent()
        {
            _isuExtraService.EnrollStudentToFlow(_flow1, _testStudent);
            _isuExtraService.RemoveStudentFromFlow(_flow1, _testStudent);
            Assert.False(_flow1.Students.Contains(_testStudent));
            Assert.False(_testStudent.AdditionalClass.Contains(_flow1));
        }

        [Test]
        public void GetStudentsTest_ListContainsStudent()
        {
            _isuExtraService.EnrollStudentToFlow(_flow1, _testStudent);
            Assert.Contains(_testStudent, _isuExtraService.GetStudents(_flow1));
        }

        [Test]
        public void GetNotEnrolledStudents_ResultContains2Students()
        {
            ExtraClassStudent testStudent2 = _isuExtraService.AddStudent(_mainGroup,"Petya");
            ExtraClassStudent testStudent3 = _isuExtraService.AddStudent(_mainGroup,"Dima");
            _isuExtraService.EnrollStudentToFlow(_flow1, _testStudent);
            List<Student> notEnrolledStudents = _isuExtraService.GetNotEnrolledStudents(_mainGroup);
            Assert.False(notEnrolledStudents.Contains(_testStudent));
            Assert.Contains(testStudent2, notEnrolledStudents);
            Assert.Contains(testStudent3, notEnrolledStudents);
        }
    }
}