using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkers.WebSockets
{
    public class GameEventHandler
    {
        Guid gameId;
        Player color;

        //TODO: add SignalR nonsense here
        //TODO: can we have instance members here?
        //some how send messages to other clients? unless we go back to my monitor thingy?

        public IOutgoingMessage establishConnection()
        {
            var gameInfo = QueueService.Instance.MatchGame();
            gameId = gameInfo.Item1;
            color = gameInfo.Item2;

            return GameManagerService.StartGame(gameId, color);
        }

        public IOutgoingMessage startTurn()
        {
            return GameManagerService.StartTurn(gameId, color);
        }

        public IOutgoingMessage makeMove(ActionDTO action)
        {
            return GameManagerService.TakeTurn(gameId, action);
        }


    }
}
