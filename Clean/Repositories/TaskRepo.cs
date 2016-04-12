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
				new Task(0, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,0))),
				new Task(1, new Address("64.047609, -21.983003", "Einivellir", null), new Time(new TimeSpan(), new DateTime(), new TimeSpan(1,1,0))),
				new Task(2, new Address("64.049362, -21.994745", "Hellnahraun", "Hafnafjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,0,10))),				
				new Task(3, new Address("64.047609, -21.983003", "Einivellir", null), new Time(new TimeSpan(), new DateTime(), new TimeSpan(1,0,0))),
				new Task(4, new Address("64.049362, -21.994745", "Hellnahraun", "Hafnafjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),				
				new Task(5, new Address("64.047609, -21.983003", "Einivellir", null), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),
				new Task(6, new Address("64.049362, -21.994745", "Hellnahraun", "Hafnafjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),				
				new Task(0, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),
				new Task(7, new Address("64.047609, -21.983003", "Einivellir", null), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),
				new Task(8, new Address("64.049362, -21.994745", "Hellnahraun", "Hafnafjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),
				new Task(9, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),
				new Task(10, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),
				new Task(11, new Address("64.045425, -21.971601", "Fjóluvellir", "Hafnarfjörður"), new Time(new TimeSpan(), new DateTime(), new TimeSpan(0,1,1))),

			};
		}
	}
}

