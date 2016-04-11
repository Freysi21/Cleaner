using System;
using Clean;

namespace Clean
{
	public class Task
	{
		public int ID { get; set; }
		public bool _status { get; set; }
		public string _comment { get; set; }
		public Address _address { get; set; }
		public Time  _time { get; set; }

		public Task(int ID, Address address, Time time)
		{
			this.ID = ID;
			this._address = address;
			this._time = time;
			this._comment = null;
			this._status = false;
		}
	}
}

