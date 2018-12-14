using System;
using System.Collections.Generic;
using System.Linq;

namespace DL444.UcquLibrary.Models
{
    public class Schedule
    {
        public List<ScheduleWeek> Weeks { get; set; } = new List<ScheduleWeek>();
        public int Count => Weeks.Count;
        public void AddWeek(ScheduleWeek week)
        {
            if(Weeks.Count < week.WeekNumber)
            {
                int originalCount = Weeks.Count;
                for (int i = 0; i < week.WeekNumber - originalCount; i++)
                {
                    Weeks.Add(new ScheduleWeek() { WeekNumber = originalCount + i + 1 });
                }
            }
            Weeks[week.WeekNumber - 1] = week;
        }
        public IList<ScheduleEntry> GetDaySchedule(int day)
        {
            List<ScheduleEntry> entries;
            int week = day / 7 + 1;
            int dayOfWeek = day % 7 + 1;
            if (week > Count || week < 1) { return new List<ScheduleEntry>(); }
            entries = (from e in (from w in Weeks where w.WeekNumber == week select w.Entries).First() where e.DayOfWeek == dayOfWeek select e).ToList();
            //entries = new List<ScheduleEntry>(this[week.ToString()].FindAll(x => x.DayOfWeek == dayOfWeek));
            return entries;
        }

    }

    public class ScheduleWeek : IComparable<ScheduleWeek>
    {
        public int WeekNumber { get; set; }

        public List<ScheduleEntry> Entries { get; set; } = new List<ScheduleEntry>();
        public int Count => Entries.Count;

        public int CompareTo(ScheduleWeek other)
        {
            return WeekNumber.CompareTo(other.WeekNumber);
        }
    }

    public class ScheduleEntry : IComparable<ScheduleEntry>
    {
        public string Name { get; set; }
        public string Lecturer { get; set; }
        public int DayOfWeek { get; set; }
        public int StartSlot { get; set; }
        public int EndSlot { get; set; }
        public string Room { get; set; }

        public ScheduleEntry(string name, string lecturer, int dayOfWeek, int startSlot, int endSlot, string room)
        {
            Name = name;
            Lecturer = lecturer;
            DayOfWeek = dayOfWeek;
            StartSlot = startSlot;
            EndSlot = endSlot;
            Room = room;
        }
        public ScheduleEntry() { }

        public int CompareTo(ScheduleEntry other)
        {
            int dayComp = DayOfWeek.CompareTo(other.DayOfWeek);
            if (dayComp == 0)
            {
                return StartSlot.CompareTo(other.StartSlot);
            }
            return dayComp;
        }

        public string SessionSpan
        {
            get
            {
                if (StartSlot == EndSlot)
                {
                    return $"{StartSlot}";
                }
                else
                {
                    return $"{StartSlot}-{EndSlot}";
                }
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
