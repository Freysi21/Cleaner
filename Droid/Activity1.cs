using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Graphics;

using Clean;

namespace Clean.Droid
{
	[Activity(Label = "AÞ-Þrif", MainLauncher = true, Icon = "@drawable/icon")]
	public class Activity1 : FragmentActivity
	{
		List<Task> tasks;
		Task currTask;
		Task detailTask;
		TimerPage Timer;
		DetailsPage Details;
		ViewPager pager;
		Button setAsTimerTaskBtn;

		//ProgressBar downloadProgress;


		protected override void OnCreate(Bundle bundle)
		{
			
			base.OnCreate(bundle);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			ActionBar.Hide ();
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			pager = FindViewById<ViewPager>(Resource.Id.pager);
			var adaptor = new GenericFragmentPagerAdaptor(SupportFragmentManager);
			tasks = TaskController.getTasks ();
			currTask = tasks [0];
			detailTask = currTask;

			pager.OffscreenPageLimit = 2;

			adaptor.AddFragmentView((i, v, b) =>
				{
					Timer = new TimerPage(v.Context, DateTime.Now, currTask);
					return Timer;
				}
			);

			adaptor.AddFragmentView((i, v, b) =>
				{
					var page = i.Inflate(Resource.Layout.Tasks,v,false);
					LinearLayout itemContainer = page.FindViewById<LinearLayout>(Resource.Id.taskList);
					foreach(Task task in tasks){
						TaskItem item = new TaskItem(v.Context, task);
						item.FindViewById<LinearLayout>(Resource.Id.taskCard).Click += (o, e) =>{
							selectTask(item.getTask());
						};
						itemContainer.AddView(item);
					}
					return page;
				}
			);

			adaptor.AddFragmentView((i, v, b) =>
				{
					Details = new DetailsPage(v.Context, detailTask);
					//var view = i.Inflate(Resource.Layout.Details, v, false);
					Details.FindViewById<Button>(Resource.Id.setAsTimerTask).Click += (sender, e) => {
						setAsTimerTask();
					};
					
					return Details;
				}
			);

			pager.Adapter = adaptor;
			pager.SetOnPageChangeListener(new ViewPageListenerForActionBar(ActionBar));

			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Timer"));
			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Tasks"));
			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Details"));

			pager.SetCurrentItem (1, false);
        }

		public void setAsTimerTask()
		{
			Timer.setTask (detailTask);
		}

		public void selectTask(Task task)
		{
			detailTask = task;
			//Timer.setTask (task);
			Details.setTask (task);
			pager.SetCurrentItem (2, true);
		}
			
		public void openMap(string coords)
		{
			var geoUri = Android.Net.Uri.Parse ("geo:42.374260,-71.120824");
			//var geoUri = Android.Net.Uri.Parse ("geo:" + coords);
			var mapIntent = new Intent (Intent.ActionView, geoUri);
			StartActivity (mapIntent);
		}
    }
}

