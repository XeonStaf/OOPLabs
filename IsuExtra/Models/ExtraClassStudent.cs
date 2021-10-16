using System.Collections.Generic;
using Isu;

namespace IsuExtra.Models
{
    public class ExtraClassStudent : Student
    {
        public ExtraClassStudent(Group @group, string name)
            : base(@group, name)
        {
            AdditionalClass = new List<ExtraGroup>();
        }

        public List<ExtraGroup> AdditionalClass { get; }
    }
}