using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Checkers.Services
{
    public class QueueService
    {
        private Guid gameId = Guid.Empty;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Guid MatchGame()
        {
            bool wasWaiting = false;

            //While there's no game, wait
            if (gameId.Equals(Guid.Empty))
            {
                wasWaiting = true;
                gameId = Guid.NewGuid();
                Monitor.Wait(this);
            }

            Guid myId = gameId;
            if (wasWaiting)
            {
                gameId = Guid.Empty;
            } else
            {
                Monitor.Pulse(this);
            }

            return myId;
        }
    }
}
