using System;
using System.Diagnostics.CodeAnalysis;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Class
    {
        public Class(string room, string teacher, int day, int startHour, int endHour, int startMinute, int endMinute)
        {
            Room = room;
            Teacher = teacher;
            ValidateTime(day, startHour, endHour, startMinute, endMinute);
            var firstMonday = new DateTime(DateTime.Now.Year, 9, 1);
            while (firstMonday.DayOfWeek != DayOfWeek.Monday)
                firstMonday = firstMonday.AddDays(1);
            StartTime = new DateTime(DateTime.Now.Year, 9, firstMonday.AddDays(day).Day, startHour, startMinute, 00);
            EndTime = new DateTime(DateTime.Now.Year, 9, firstMonday.AddDays(day).Day, endHour, endMinute, 00);
        }

        public string Room { get; set; }
        public string Teacher { get; set; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public bool CheckOverlap(Class other)
        {
            return (EndTime >= other.StartTime || StartTime >= other.StartTime) &&
                   (other.EndTime >= StartTime || other.StartTime >= StartTime);
        }

        private void ValidateTime(int day, int startHour, int endHour, int startMinute, int endMinute)
        {
            if (day is < 0 or > 6)
                throw new IsuExtraException("Invalid day");
            if (startHour > endHour)
                throw new IsuExtraException("Invalid Start and End hour");
            if (startHour == endHour && startMinute > endMinute)
                throw new IsuExtraException("Invalid Start and End minutes");
        }
    }
}