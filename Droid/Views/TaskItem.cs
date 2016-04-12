
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
	public class TaskItem : LinearLayout
	{
		private Task _task;

		public TaskItem (Context context, Task task) :
			base (context)
		{
			this._task = task;
			Initialize (context);
		}

		void Initialize (Context context)
		{
			Inflate (context, Resource.Layout.Task, this);
			TextView itemTitle = FindViewById<TextView> (Resource.Id.itemTitle);
			itemTitle.SetText (this._task._address._address, TextView.BufferType.Normal);
			TextView itemSubTitle = FindViewById<TextView> (Resource.Id.itemSubTitle);
			itemSubTitle.SetText (this._task._address._city, TextView.BufferType.Normal);
		}
		public Task getTask()
		{
			return this._task;
		}
	}
}

