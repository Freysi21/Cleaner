using System;

namespace Clean
{
	public class Time
	{
		public TimeSpan _timeSpan{ get; set; }
		public DateTime _dateTime { get; set; }
		public TimeSpan _estimate { get; set; }
		public int _secs { get; set; }

		public Time(TimeSpan timeSpan, DateTime dateTime, TimeSpan estimate, int secs)
		{
			this._timeSpan = timeSpan;
			this._dateTime = dateTime;
			this._estimate = estimate;
			this._secs = secs;
		}
		public Time(TimeSpan timeSpan, DateTime dateTime, TimeSpan estimate)
		{
			this._timeSpan = timeSpan;
			this._dateTime = dateTime;
			this._estimate = estimate;
			this._secs = Convert.ToInt32(new Random ().NextDouble() * (1000 * (new Random ().NextDouble() * 100))); 
		}
	}
}

