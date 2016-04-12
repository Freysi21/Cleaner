
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
using Android.Support.Graphics.Drawable;

namespace Clean.Droid
{
	public class TimerPage : RelativeLayout
	{
		private Task _currTask;


		private TextView _time = FindViewById<TextView> (Resource.Id.time);
		private TextView _estimate;
		private TextView _cardTitle;
		private TextView _cardSubTitle;

		private TimeSpan _span;
		private TaskItem _viewTask;
		private System.Timers.Timer _timer;

		private Button _continueBtn;
		private Button _stopBtn;
		private Button _startBtn1;
		private Button _startBtn2;

		private LinearLayout _btnGroup1;
		private LinearLayout _btnGroup2;
		private LinearLayout _btnGroup3;
		private LinearLayout _prb;
		private LinearLayout _continueButton;

		private int _numOfBars;
		private int _countSeconds;

		public TimerPage (Context context, DateTime time, Task currTask) :
			base (context)
		{
			Initialize (context, time, currTask);
		}

		void Initialize (Context context, DateTime time, Task currTask)
		{
			Inflate (context, Resource.Layout.Timer, this);

			/*For task card and progress estimate*/
			this._cardTitle = FindViewById<TextView> (Resource.Id.itemTitle);
			this._cardSubTitle = FindViewById<TextView> (Resource.Id.itemSubTitle);
			this._estimate = FindViewById<TextView> (Resource.Id.estimate);

			this._timer = new System.Timers.Timer();

			setupProgressBar ();
			setupButtons ();
			setTask (currTask);
		}

		public void startTimer(object sender, EventArgs e)
		{
			int count = this._prb.ChildCount;
			for(int i = 0; i < this._prb.ChildCount; i++)
			{
				this._prb.GetChildAt (i).Visibility = ViewStates.Invisible;
			}
			this._time.SetText ("00:00:00", TextView.BufferType.Normal);
			this._timer.Interval = 1000;
			this._timer.Elapsed += OnTimedEvent;
			this._countSeconds = 0;
			this._timer.Enabled = true;
			this._btnGroup1.Visibility = ViewStates.Gone;
			this._btnGroup2.Visibility = ViewStates.Visible;
		}

		public void continueTimer(object sender, EventArgs e)
		{
			this._timer.Enabled = true;
			this._btnGroup3.Visibility = ViewStates.Gone;
			this._btnGroup2.Visibility = ViewStates.Visible;


		}

		public void stopTimer(object sender, EventArgs e)
		{
			this._timer.Enabled = false;
			this._btnGroup3.Visibility = ViewStates.Visible;
			this._btnGroup2.Visibility = ViewStates.Gone;

		}

		private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
		{

			this._countSeconds++;
			this._currTask._time._secs = this._countSeconds;
			int bars = 0;
			if (this._currTask._time._estimate.Ticks > 0) {
				double progressRatio = this._currTask._time._timeSpan.Ticks / (double)this._currTask._time._estimate.Ticks;
				progressRatio = progressRatio * 10;
				progressRatio = Math.Floor (progressRatio);
				bars = Convert.ToInt32 (progressRatio);
			}

			//here backslash is must to tell that colon is
			//not the part of format, it just a character that we want in output
			string str = this._currTask._time._timeSpan.ToString(@"hh\:mm\:ss");
			bars = bars;
			((Activity)this.Context).RunOnUiThread (() => {
				if(this._time.Text != str)
				{
					this._time.SetText (str, TextView.BufferType.Normal);
				}
				if(bars > this._numOfBars && bars < 10)
				{
					this._numOfBars = bars;
					for(int i = 0; i < bars; i++)
					{
						this._prb.GetChildAt(i).Visibility = ViewStates.Visible;
					}
				}
				else if(bars == 10)
				{
					this._numOfBars = bars;
					this._prb.GetChildAt(bars - 1).Visibility = ViewStates.Visible;
				}
			});

		}

		public void setTask(Task task)
		{
			if (!this._timer.Enabled) {
				this._currTask = task;
				this._countSeconds = this._currTask._time._secs;
				this._estimate.SetText ("Estimated time: " + this._currTask._time._estimate.ToString(@"hh\:mm\:ss"), TextView.BufferType.Normal);
				this._cardTitle.SetText (this._currTask._address._address, TextView.BufferType.Normal);
				this._cardSubTitle.SetText (this._currTask._address._city, TextView.BufferType.Normal);

				this._btnGroup3.Visibility = ViewStates.Gone;
				this._btnGroup2.Visibility = ViewStates.Gone;
				this._btnGroup1.Visibility = ViewStates.Visible;

			}
		}

		public Task getTask()
		{
			return this._currTask;
		}

		public void setupButtons()
		{
			this._startBtn1 = FindViewById<Button> (Resource.Id.startTimer1);
			this._startBtn2 = FindViewById<Button> (Resource.Id.startTimer2);
			this._stopBtn = FindViewById<Button> (Resource.Id.stopTimer);
			this._continueBtn = FindViewById<Button> (Resource.Id.continueTimer);

			this._btnGroup1 = FindViewById<LinearLayout> (Resource.Id.buttonGroup1);
			this._btnGroup2 = FindViewById<LinearLayout> (Resource.Id.buttonGroup2);
			this._btnGroup3 = FindViewById<LinearLayout> (Resource.Id.buttonGroup3);

			this._continueBtn.Click += continueTimer;
			this._startBtn1.Click += startTimer;
			this._startBtn2.Click += startTimer;
			this._stopBtn.Click += stopTimer;
		}

		public void setupProgressBar()
		{
			this._countSeconds = 0;
			this._numOfBars = -1;
			this._prb = FindViewById<LinearLayout> (Resource.Id.progressBar);
		}
	}
}

