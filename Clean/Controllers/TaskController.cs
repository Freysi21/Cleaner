using System;
using System.Collections.Generic;
using Clean;
namespace Clean
{
	public static class TaskController
	{
		private static TaskRepo _tRepo = new TaskRepo();
		private static Task currTask = _tRepo.getFirst();
		public static Task getTask(int Id)
		{
			return _tRepo.getTask (Id);
		}
		public static List<Task> getTasks()
		{
			return _tRepo.getTasks ();
		}

		public static void setTask(Task task)
		{
			_tRepo.setTask (task);
		}

		public static void setCurrentTask(Task task)
		{
			currTask = task;
		}

		public static Task getCurrentTask()
		{
			return currTask;
		}
	}
}

