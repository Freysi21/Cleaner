
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Clean;

namespace Clean.Droid
{
	public class DetailsPage : RelativeLayout
	{
		private Task _task;

		public DetailsPage (Context context, Task task) :
			base (context)
		{
			this._task = task;
			Initialize (context);
		}

		void Initialize (Context context)
		{
		}
	}
}

