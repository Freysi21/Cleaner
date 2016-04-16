using System;
using Clean;

namespace Clean
{
	public class Task
	{
		public int ID { get; set; }
		public bool _status { get; set; }
		public string _comment { get; set; }
		public string _image { get; set; }
		public Address _address { get; set; }
		public Time  _time { get; set; }

		public Task(int ID, Address address, Time time, string image)
		{
			this.ID = ID;
			this._address = address;
			this._time = time;
			this._comment = null;
			this._image = image;
			this._status = false;
		}
	}
}

