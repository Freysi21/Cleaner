
using System;
using System.Diagnostics;
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
using Android.Graphics;

namespace Clean.Droid
{
	public class TimerPage : RelativeLayout
	{
		private TextView _time;
		private TimeSpan _span;
		private System.Timers.Timer _timer;
		private int _countSeconds;
		private Button _btn;

		public TimerPage (Context context, DateTime time) :
			base (context)
		{
			Initialize (context, time);
		}

		void Initialize (Context context, DateTime time)
		{
			Inflate (context, Resource.Layout.Timer, this);
			this._time = FindViewById<TextView> (Resource.Id.time);
			this._countSeconds = 0;

			this._btn = FindViewById<Button> (Resource.Id.startTimer);
			this._btn.Click += startTimer;

		}

		public void startTimer(object sender, EventArgs e)
		{
			this._timer = new System.Timers.Timer();
			this._timer.Interval = 1000;
			this._timer.Elapsed += OnTimedEvent;
			this._countSeconds = 0;
			this._timer.Enabled = true;
			this._btn.SetText("Stop", TextView.BufferType.Normal);
			this._btn.Click -= startTimer;
			this._btn.Click += stopTimer;

			//TODO:Send to controller time.

		}

		public void stopTimer(object sender, EventArgs e)
		{
			this._timer.Enabled = false;
			this._btn.SetText("Start", TextView.BufferType.Normal);
			this._btn.Click -= stopTimer;
			this._btn.Click += startTimer;
		}

		private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
		{

			this._countSeconds++;
			this._span = TimeSpan.FromSeconds(this._countSeconds);

			//here backslash is must to tell that colon is
			//not the part of format, it just a character that we want in output
			string str = this._span.ToString(@"hh\:mm\:ss");

			((Activity)this.Context).RunOnUiThread (() => {
				this._time.SetText (str, TextView.BufferType.Normal);
			});

		}

	}
}

