using AuroraEmu.Game;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AuroraEmu.DI.Game
{
    public interface ITaskController
    {
        Task ExecuteOnce(IAuroraTask executeTask, int delay = 0);

        ActionBlock<DateTimeOffset> ExecutePeriodic(Action<DateTimeOffset> action,
            CancellationToken cancellationToken, int delay);
    }
}
