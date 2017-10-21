using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using AuroraEmu.Game.Rooms.Models;
using AuroraEmu.Network.Game.Packets.Composers.Users;
using AuroraEmu.Game.Items.Models;

namespace AuroraEmu.Game.Rooms.Components
{
    public class ProcessComponent : IDisposable
    {
        private Room room;
        private CancellationTokenSource _wtoken;
        private ActionBlock<DateTimeOffset> _task;
        private ProcessActor actorProcessor;
        private ProcessRoller rollerProcessor;
        private ProcessItems itemsProcessor;

        private int idleRoom = 0;
        private int rollerTick = 0;
        private int itemsTick = 0;

        public ProcessComponent(Room room)
        {
            this.room = room;
            actorProcessor = new ProcessActor();
            rollerProcessor = new ProcessRoller();
            itemsProcessor = new ProcessItems();
        }

        public void SetupRoomLoop()
        {
            _wtoken = new CancellationTokenSource();

            _task = Engine.Locator.TaskController.ExecutePeriodic(now => Loop(), _wtoken.Token, 500);
            _task.Post(DateTimeOffset.Now);
        }

        private void Loop()
        {
            room.Loop();

            if (idleRoom > 120) room.Dispose();

            if (!(room.Actors.Count > 0))
                idleRoom++;
            else
                idleRoom = 0;

            if (itemsTick == 2)
            {
                itemsProcessor.Process(room);

                itemsTick = 0;
            }

            if (rollerTick == 4) { // 2 seconds
                List<Item> rollers = this.room.GetItems("roller");
                
                if (rollers != null)
                    rollerProcessor.Process(rollers, room);

                rollerTick = 0;
            }
            
            rollerTick++;
            itemsTick++;

            List<RoomActor> toUpdate = new List<RoomActor>();
            foreach (RoomActor actor in room.Actors.Values)
            {
                actorProcessor.Process(actor, room);
                if (!actor.UpdateNeeded || toUpdate.Contains(actor))
                    continue;

                actor.UpdateNeeded = false;
                toUpdate.Add(actor);
            }

            if (toUpdate.Count > 0)
                room.SendComposer(new UserUpdateMessageComposer(toUpdate));
        }

        public void Dispose()
        {
            _wtoken.Cancel();
            _task = null;
            _wtoken = null;
        }
    }
}