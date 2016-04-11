using System;

namespace Clean
{
	/// <summary>
	/// Address of workers next task
	/// </summary>
	public class Address
	{
		public string _coords { get; set; }
		public string _address { get; set; }
		public string _city {get; set;}

		public Address(string coords, string address, string city)
		{
			this._coords = coords;
			this._address = address;
			this._city = city;
		}
	}
}

