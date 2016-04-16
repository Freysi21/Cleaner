
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using Clean;

namespace Clean.Droid
{
	public class DetailsPage : RelativeLayout
	{
		private Task _task;
		private int count = 1;
		private WebClient webClient;
		private TextView detailsContentTitle;
		private TextView detailsContent;
		private ImageView imageview;
		private ImageView googleMaps;


		public DetailsPage (Context context, Task task) :
			base (context)
		{
			_task = task;
			Initialize (context);
		}

		void Initialize (Context context)
		{
			Inflate (context, Resource.Layout.Details, this);
			imageview = this.FindViewById<ImageView>(Resource.Id.taskImage);
			googleMaps = this.FindViewById<ImageView>(Resource.Id.googleMaps);
			googleMaps.Click += openMap;

			detailsContentTitle = this.FindViewById<TextView> (Resource.Id.detailsContentTitle);
			detailsContent = this.FindViewById<TextView> (Resource.Id.detailsContent);

			setContent ();
		}

		public void setContent()
		{
			if(_task._address._city != null && _task._address._city.Length > 0)
			{
				detailsContentTitle.SetText(_task._address._address + " - " + _task._address._city, TextView.BufferType.Normal);
			}
			else 
			{
				detailsContentTitle.SetText(_task._address._address, TextView.BufferType.Normal);
			}
			downloadAsync(_task._image);
		}

		public void setTask(Task task)
		{
			_task = task;
			setContent ();

		}

		public void openMap(object sender, EventArgs e)
		{
			var geoUri = Android.Net.Uri.Parse ("geo:" + _task._address._coords + "?q=" + _task._address._address);
			//var geoUri = Android.Net.Uri.Parse ("geo:" + coords);
			var mapIntent = new Intent (Intent.ActionView, geoUri);
			this.Context.StartActivity (mapIntent);
		}
		async void downloadAsync(string src)
		{
			webClient = new WebClient ();
			var url = new Uri (src);
			byte[] bytes = null;

			webClient.DownloadProgressChanged += HandleDownloadProgressChanged;

			try{
				bytes = await webClient.DownloadDataTaskAsync(url);
			}
			catch(TaskCanceledException){
				Console.WriteLine ("Task Canceled!");
				return;
			}
			catch(Exception e){
				Console.WriteLine (e.ToString());
				//this.downloadProgress.Progress = 0;
				return;
			}
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);	
			string localFilename = "downloaded.png";
			string localPath = System.IO.Path.Combine (documentsPath, localFilename);

			FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
			await fs.WriteAsync (bytes, 0, bytes.Length);

			Console.WriteLine("localPath:"+localPath);
			fs.Close ();

			BitmapFactory.Options options = new BitmapFactory.Options ();
			options.InJustDecodeBounds = true;
			await BitmapFactory.DecodeFileAsync (localPath, options);

			options.InSampleSize = options.OutWidth > options.OutHeight ? options.OutHeight / imageview.Height : options.OutWidth / imageview.Width;
			options.InJustDecodeBounds = false;

			Bitmap bitmap = await BitmapFactory.DecodeFileAsync (localPath, options);

			Console.WriteLine ("Loaded!");

			imageview.SetImageBitmap (bitmap);

			//this.downloadProgress.Progress = 0;
		}

		void HandleDownloadProgressChanged (object sender, DownloadProgressChangedEventArgs e)
		{
			//this.downloadProgress.Progress = e.ProgressPercentage;
		}

		void cancelDownload(object sender, System.EventArgs ea)
		{
			Console.WriteLine ("Cancel clicked!");
			if(webClient!=null)
				webClient.CancelAsync ();

			webClient.DownloadProgressChanged -= HandleDownloadProgressChanged;

			//this.downloadProgress.Progress = 0;

		}
	}
}

