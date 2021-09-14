namespace Isu
{
    public class Student
    {
        private static int _counter = 0;
        public Student(Group group, string name)
        {
            Group = group;
            Name = name;
            Id = _counter;
            _counter++;
        }

        public Group Group { get; set; }
        public string Name { get; }
        public int Id { get; }
    }
}