using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Checkers.Services
{
    public class QueueService
    {
        private static readonly QueueService instance = new QueueService();

        static QueueService()
        {
        }

        private QueueService()
        {
        }

        public static QueueService Instance
        {
            get
            {
                return instance;
            }
        }

        private Guid gameId = Guid.Empty;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public (Guid, Player) MatchGame()
        {
            bool wasWaiting = false;

            //While there's no game, wait
            while (gameId.Equals(Guid.Empty))
            {
                wasWaiting = true;
                gameId = Guid.NewGuid();
                Monitor.Wait(this);
            }

            Guid myId = gameId;
            Player myColor;
            if (wasWaiting)
            {
                gameId = Guid.Empty;
                myColor = Player.BLACK;
            }
            else
            {
                Monitor.Pulse(this);
                myColor = Player.RED;
            }

            return (myId, myColor);
        }
    }
}
