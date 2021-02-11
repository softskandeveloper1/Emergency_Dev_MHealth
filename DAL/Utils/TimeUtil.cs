using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Utils
{
    public static class TimeUtil
    {
        public static List<string> split_time()
        {
            var quarterHours = new[] { "00", "30" };
            var times = new List<string>();
            for (var i = 0; i < 24; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    var time = i + ":" + quarterHours[j];
                    if (i < 10)
                    {
                        time = "0" + time;
                    }
                    times.Add(time);
                }
            }

            return times;
        }

        public static List<string> split_time(DateTime start_date,DateTime enddate)
        {
            var quarterHours = new[] { "00", "30" };
            var times = new List<string>();
            for (var i = 0; i < 24; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    var time = i + ":" + quarterHours[j];
                    if (i < 10)
                    {
                        time = "0" + time;
                    }
                    times.Add(time);
                }
            }

            return times;
        }

        public static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }


        public static IEnumerable<Tuple<DateTime, DateTime>> SplitDateRange(DateTime start, DateTime end, int chunkSize)
        {
            DateTime chunkEnd;
            while ((chunkEnd = start.AddMinutes(chunkSize)) < end)
            {
                yield return Tuple.Create(start, chunkEnd);
                start = chunkEnd;
            }
            yield return Tuple.Create(start, end);
        }
    }
}
