using AuroraEmu.DI.Game;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AuroraEmu.Game
{
    public class TaskController : ITaskController
    {
        public Task ExecuteOnce(IAuroraTask executeTask, int delay = 0)
        {
            if (delay == 0)
            {
                return Task.Run(() => executeTask.Execute());
            }
            return Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
                executeTask.Execute();
            });
        }

        public ActionBlock<DateTimeOffset> ExecutePeriodic(Action<DateTimeOffset> action,
            CancellationToken cancellationToken, int delay)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            ActionBlock<DateTimeOffset> block = null;

            block = new ActionBlock<DateTimeOffset>(async now =>
            {
                action(now);

                await Task.Delay(TimeSpan.FromMilliseconds(delay), cancellationToken).
                    ConfigureAwait(false);

                block.Post(DateTimeOffset.Now);
            }, new ExecutionDataflowBlockOptions
            {
                CancellationToken = cancellationToken
            });

            return block;
        }
    }
}
