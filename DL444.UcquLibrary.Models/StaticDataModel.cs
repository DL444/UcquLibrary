using System;
using System.Collections.Generic;

namespace DL444.UcquLibrary.Models
{
    public class StaticDataModel
    {
        public string CurrentTerm { get; set; }
        public DateTime StartDate { get; set; }

        public List<ScheduleTime> StartTimeABC { get; set; }
        public List<ScheduleTime> EndTimeABC { get; set; }
        public List<ScheduleTime> StartTimeD { get; set; }
        public List<ScheduleTime> EndTimeD { get; set; }
    }

    public struct ScheduleTime
    {
        private TimeSpan ts;

        public int Hour => ts.Hours;
        public int Minute => ts.Minutes;

        public ScheduleTime(int hour, int minute)
        {
            ts = new TimeSpan(hour, minute, 0);
        }

        public DateTime GetDateTime()
        {
            return DateTime.Today + ts;
        }

        public override string ToString()
        {
            return $"{Hour}:{Minute:00}";
        }
    }
}
