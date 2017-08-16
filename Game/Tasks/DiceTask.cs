using System;

namespace AuroraEmu.Game.Tasks
{
    public class DiceTask : IAuroraTask
    {
        private readonly int _number;

        public DiceTask(int number)
        {
            _number = number;
        }

        public void Execute()
        {
            Console.WriteLine(_number);
        }
    }
}
