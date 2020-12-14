using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Logging;

namespace MooMed.DotNet.Utils.Tasks
{
    public static class FireAndForgetTask
    {
        /// <summary>
        /// Raises a fire and forget task
        /// </summary>
        /// <param name="taskGenerator"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static async Task Run(
            [NotNull] Func<Task> taskGenerator,
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

        /// <summary>
        /// Raises a fire and forget task on the ThreadPool
        /// </summary>
        /// <param name="taskGenerator"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static async Task RunThreadPool(
            [NotNull] Func<Task> taskGenerator,
            IMooMedLogger logger)
        {
            try
            {
                await Task.Run(taskGenerator);
            }
            catch (Exception e)
            {
                logger.Exception(e);
            }
        }
    }
}