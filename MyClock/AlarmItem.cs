using System;

using SQLite;

namespace MyClock
{
    public class AlarmItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public TimeSpan Time { get; set; }

        public string TimeString { get; set; }      // For putting a stringified copy of Time, to solve the bug of "time" missing in AlarmPage.

        public bool Work { get; set; }

        public string Note { get; set; }

        public override string ToString()
        {
            return Time.ToString();
        }
    }
}
