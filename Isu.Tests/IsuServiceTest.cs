using System;
using Isu.Services;
using Isu.Tools;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3205");
            Student student = _isuService.AddStudent(group,"Test");
            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group group = _isuService.AddGroup("M3205");
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < 26; i++)
                    _isuService.AddStudent(group, "TestGroup" + i.ToString());
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("Hello!");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group oldGroup = _isuService.AddGroup("M3205");
            Group newGroup = _isuService.AddGroup("M3306");
            Student student = _isuService.AddStudent(oldGroup, "Ivan");
            _isuService.ChangeStudentGroup(student, newGroup);
            Assert.Contains(student, newGroup.Students);
            Assert.IsFalse(oldGroup.Students.Contains(student));
        }
    }
}