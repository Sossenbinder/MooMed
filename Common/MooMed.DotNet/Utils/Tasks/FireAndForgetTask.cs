using System;
using System.Threading.Tasks;
using MooMed.Logging.Abstractions.Interface;

namespace MooMed.DotNet.Utils.Tasks
{
	public class FireAndForgetTask
	{
		public bool IsCompleted => UnderlyingTask.IsCompleted;

		public Task UnderlyingTask { get; }

		private FireAndForgetTask(Task task)
		{
			UnderlyingTask = task;
		}

		/// <summary>
		/// Raises a fire and forget task
		/// </summary>
		/// <param name="taskGenerator"></param>
		/// <param name="logger"></param>
		/// <returns></returns>
		public static FireAndForgetTask Run(
			Func<Task> taskGenerator,
			IMooMedLogger logger)
		{
			var task = RunInternal(taskGenerator, logger);

			return new FireAndForgetTask(task);
		}

		/// <summary>
		/// Raises a fire and forget task on the ThreadPool
		/// </summary>
		/// <param name="taskGenerator"></param>
		/// <param name="logger"></param>
		/// <returns></returns>
		public static FireAndForgetTask RunThreadPool(
			Func<Task> taskGenerator,
			IMooMedLogger logger)
		{
			var task = Task.Run(() => RunInternal(taskGenerator, logger));

			return new FireAndForgetTask(task);
		}

		private static async Task RunInternal(
			Func<Task> taskGenerator,
			IMooMedLogger logger)
		{
			try
			{
				await taskGenerator();
			}
			catch (Exception e)
			{
				logger.Exception(e);
			}
		}
	}
}