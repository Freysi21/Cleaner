
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

namespace Clean.Droid
{
	public class TasksPage : LinearLayout
	{
		private List<Task> _tasks;
		public TasksPage (Context context, List<Task> tasks) :
			base (context)
		{
			this._tasks = tasks;

			Initialize (context);
		}
			
		void Initialize (Context context)
		{
			Inflate (context, Resource.Layout.Tasks, this);

			foreach (Task task in this._tasks) {
				TaskItem view = new TaskItem (context, task);
				this.AddView (view);
			}

		}
	}
}

