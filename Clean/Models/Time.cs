using System;

namespace Clean
{
	public class Time
	{
		public TimeSpan _timeSpan{ get; set; }
		public DateTime _dateTime { get; set; }

		public Time(TimeSpan timeSpan, DateTime dateTime)
		{
			this._timeSpan = timeSpan;
			this._dateTime = dateTime;

		}
	}
}

