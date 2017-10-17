namespace AuroraEmu.Utilities.Queue
{
    public class FastPriorityQueueNode
    {
        public float Priority { get; protected internal set; }

        public int QueueIndex { get; internal set; }
    }
}