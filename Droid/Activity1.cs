using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Clean;

namespace Clean.Droid
{
	[Activity(Label = "AÞ-Þrif", MainLauncher = true, Icon = "@drawable/icon")]
	public class Activity1 : FragmentActivity
	{
		List<Task> tasks;
		Task currTask;
		TimerPage Timer;
		protected override void OnCreate(Bundle bundle)
		{
			
			base.OnCreate(bundle);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			ActionBar.Hide ();
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			var pager = FindViewById<ViewPager>(Resource.Id.pager);
			var adaptor = new GenericFragmentPagerAdaptor(SupportFragmentManager);
			tasks = TaskController.getTasks ();
			currTask = tasks [0];

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
					var view = i.Inflate(Resource.Layout.Details, v, false);
					return view;
				}
			);

			pager.Adapter = adaptor;
			pager.SetOnPageChangeListener(new ViewPageListenerForActionBar(ActionBar));

			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Timer"));
			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Tasks"));
			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Details"));

			pager.SetCurrentItem (1, false);
        }

		public void selectTask(Task task)
		{
			Timer.setTask (task);
		}
    }
}

