using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean;

namespace Clean
{
	public class TaskRepo
	{
		public static List<Task> taskList;
		public TaskRepo ()
		{
			//taskList = new List<Task> ();
			taskList = setTasks();
		}

		public Task getTask(int id)
		{
			return taskList.Where (x => x.ID == id).Select (x => x).FirstOrDefault ();
		}

		public List<Task> getTasks()
		{
			return taskList;
		}

		public void setTask(Task task)
		{
			taskList [task.ID] = task;
		}

		public Task getFirst()
		{
			return taskList [0];
		}

		public List<Task> setTasks()
		{
			return new List<Task> {
				new Task(0, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,0), 10^5), "http://www.verkis.is/media/verkefni/large/austur_laekjargata.jpg"),
				new Task(1, new Address("64.047609, -21.983003", "Einivellir", null), new Time(new TimeSpan(), new DateTime(), new TimeSpan(1,1,0), 10^5), "http://object.is/myobject/wp-content/uploads/2010/10/IMG_5004.jpg"),
				new Task(2, new Address("64.049362, -21.994745", "Hellnahraun", "Hafnafjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,0,10), 10^5), "http://emilhannes.blog.is/users/03/emilhannes/img/haskolinn.jpg"),				
				new Task(3, new Address("64.047609, -21.983003", "Einivellir", null), new Time(new TimeSpan(), new DateTime(), new TimeSpan(1,0,0), 10^5), "http://www.verkis.is/media/thjonusta/large/byggingar.jpg"),
				new Task(4, new Address("64.049362, -21.994745", "Hellnahraun", "Hafnafjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5),"http://svalir.is/wp-content/uploads/2012/09/husid-before-after.jpg"),				
				new Task(5, new Address("64.047609, -21.983003", "Einivellir", null), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5), "http://www.verkis.is/media/thjonusta/large/stofnun_vigdisar.jpg"),
				new Task(6, new Address("64.049362, -21.994745", "Hellnahraun", "Hafnafjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5),"http://www.mbl.is/tncache/frimg/dimg_cache/e360x273/6/66/666001.jpg"),				
				new Task(0, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5), "http://eyjan.pressan.is/silfuregils/wp-content/uploads/2013/02/2e5721e5d38c3262ac086ea004c743b6.jpg?f5df2f"),
				new Task(7, new Address("64.047609, -21.983003", "Einivellir", null), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5), "http://www.visir.is/apps/pbcsi.dll/storyimage/XZ/20131017/FRETTIR01/710179927/AR/0/AR-710179927.jpg?NoBorder"),
				new Task(8, new Address("64.049362, -21.994745", "Hellnahraun", "Hafnafjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5), "http://lemurinn.is/wp-content/uploads/2014/05/800px-pieter_bruegel_the_elder_-_the_tower_of_babel_vienna_-_google_art_project_-_edited.jpg"),
				new Task(9, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5), "http://veturvor.heimsferdir.is/heimsferdir/upload/images/afangastadir/rom/shutterstock_110457797.jpg"),
				new Task(10, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5), "http://travels.kilroy.is/media/8556984/barcelona-las-ramblas_517x291.jpg"),
				new Task(11, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1), 10^5), "http://www.dv.is/media/cache/03/c0/03c05a908126c65098e73a9d52b16392.jpg"),

			};
		}
	}
}

