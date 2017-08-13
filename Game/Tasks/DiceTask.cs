using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraEmu.Game.Tasks
{
    public class DiceTask : AuroraTask
    {
        int number;

        public DiceTask(int number)
        {
            this.number = number;
        }

        public void Execute()
        {
            Console.WriteLine(number);
        }
    }
}
