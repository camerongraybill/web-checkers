using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Checkers.WebSockets
{
    public class GameEventHandler : Hub
    {
        Guid gameId;
        Player color;

        //TODO: add SignalR nonsense here
        //TODO: can we have instance members here?
        //some how send messages to other clients? unless we go back to my monitor thingy?

        public async Task onMove(string move_resonse)
        {
            var response = GameManagerService.TakeTurn(gameId, ActionDTO.Deserialize(move_resonse));
            if (response is EndGameDTO)
            {
                
            }
            else
            {
                var casted_response = (TurnDTO) response;
                if (casted_response.moves.Count != 0)
                {
                    await Clients.Caller.SendAsync("yourMove", casted_response.Serialize());
                }
                else
                {
                    
                }
            }
            

        } 

        public override async Task OnConnectedAsync()
        {
            var gameInfo = QueueService.Instance.MatchGame();
            gameId = gameInfo.Item1;
            color = gameInfo.Item2;

            var response = GameManagerService.StartGame(gameId, color, "userID");
            await Clients.Caller.SendAsync("gameStart", response.Serialize());
            if (color == Player.BLACK)
            {
                await Clients.Caller.SendAsync("yourMove", GameManagerService.StartTurn(gameId, color).Serialize());
            }
            await base.OnConnectedAsync();
        }
    }
}
