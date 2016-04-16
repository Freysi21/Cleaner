
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

using Clean;

namespace Clean.Droid
{
	public class TimerPage : RelativeLayout
	{
		private Task _currTask;


		private TextView _time;
		private TextView _estimate;
		private TextView _cardTitle;
		private TextView _cardSubTitle;

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
			_currTask = currTask;
			_timer = new System.Timers.Timer();
			_timer.Interval = 1000;
			_timer.Elapsed += OnTimedEvent;
			_countSeconds = 0;
			_numOfBars = -1;


			Initialize (context, time, currTask);
		}

		void Initialize (Context context, DateTime time, Task currTask)
		{
			Inflate (context, Resource.Layout.Timer, this);

			/* Time related */
			_estimate = FindViewById<TextView> (Resource.Id.estimate);

			/* Button related */
			_btnGroup1 = FindViewById<LinearLayout> (Resource.Id.buttonGroup1);
			_btnGroup2 = FindViewById<LinearLayout> (Resource.Id.buttonGroup2);
			_btnGroup3 = FindViewById<LinearLayout> (Resource.Id.buttonGroup3);

			_stopBtn = FindViewById<Button> (Resource.Id.stopTimer);
			_stopBtn.Click += stopTimer;

			_startBtn1 = FindViewById<Button> (Resource.Id.startTimer1);
			_startBtn2 = FindViewById<Button> (Resource.Id.startTimer2);
			_startBtn1.Click += startTimer;
			_startBtn2.Click += startTimer;

			_continueBtn = FindViewById<Button> (Resource.Id.continueTimer);
			_continueBtn.Click += continueTimer;


			/* Card related */
			_cardTitle = FindViewById<TextView> (Resource.Id.itemTitle);
			_cardSubTitle = FindViewById<TextView> (Resource.Id.itemSubTitle);

			_prb = FindViewById<LinearLayout> (Resource.Id.progressBar);

			setTask (currTask);
		}

		public void startTimer(object sender, EventArgs e)
		{
			int count = this._prb.ChildCount;
			for(int i = 0; i < this._prb.ChildCount; i++)
			{
				this._prb.GetChildAt (i).Visibility = ViewStates.Invisible;
			}
			this._estimate.SetText ("Estimated time: " + this._currTask._time._estimate.ToString(@"hh\:mm\:ss") + "/00:00:00", TextView.BufferType.Normal);

			//this._time.SetText ("00:00:00", TextView.BufferType.Normal);
			this._numOfBars = -1;
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
			TaskController.setTask (this._currTask);
			this._timer.Enabled = false;
			this._btnGroup3.Visibility = ViewStates.Visible;
			this._btnGroup2.Visibility = ViewStates.Gone;

		}

		private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
		{

			this._countSeconds++;
			this._currTask._time._secs = this._countSeconds;
			int bars = 0;

			if (this._currTask._time._estimate.TotalSeconds > 0) {
				double progressRatio = this._countSeconds / this._currTask._time._estimate.TotalSeconds;
				progressRatio = progressRatio * 10;
				progressRatio = Math.Floor (progressRatio);
				bars = Convert.ToInt32 (progressRatio);
			}
				
			this._currTask._time._timeSpan = new TimeSpan(0,0,this._countSeconds);
			string str = "Estimated time: " + this._currTask._time._estimate.ToString(@"hh\:mm\:ss") + "/" +this._currTask._time._timeSpan.ToString(@"hh\:mm\:ss");

			((Activity)this.Context).RunOnUiThread (() => {
				if(this._estimate.Text != str)
				{
					this._estimate.SetText (str, TextView.BufferType.Normal);
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
				this._estimate.SetText ("Estimated time: " + this._currTask._time._estimate.ToString(@"hh\:mm\:ss") + "/", TextView.BufferType.Normal);
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
	}
}

