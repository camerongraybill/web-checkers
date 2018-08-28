using Checkers.Models;
using Checkers.Services;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Checkers.WebSockets
{
    public class GameEventHandler : Hub
    {
        private Guid gameId
        {
            get { return GameManagerService.GetGameId(Context.ConnectionId); }
        }

        private Player color
        {
            get { return GameManagerService.GetColor(Context.ConnectionId);  }
        }

        private string OpponentId
        {
            get { return GameManagerService.GetUserId(gameId, OpponentColor); }
        }

        private Player OpponentColor
        {
            get { return color == Player.BLACK ? Player.RED : Player.BLACK; }
        }

        public async Task onMove(string move_resonse)
        {
            Console.Write("ADAM RECIEVED:  " + move_resonse);
            var response = GameManagerService.TakeTurn(gameId, ActionDTO.Deserialize(move_resonse));
            if (response is EndGameDTO)
            {
                var casted_response = (EndGameDTO) response;
                await Clients.Clients(Context.ConnectionId, OpponentId)
                    .SendAsync("gameEnd", casted_response.Serialize());
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
                    await Clients.Client(OpponentId)
                        .SendAsync("yourMove", GameManagerService.StartTurn(gameId, OpponentColor).Serialize());
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Client(OpponentId).SendAsync("gameEnd", new EndGameDTO()
            {
                reason = EndReason.OPPONENT_DISCONNECT,
                winner = OpponentColor
            }.Serialize());
            await base.OnDisconnectedAsync(exception);
        }
        
        public override async Task OnConnectedAsync()
        {
            var gameInfo = QueueService.Instance.MatchGame();

            var response = GameManagerService.StartGame(gameInfo.Item1, gameInfo.Item2, Context.ConnectionId);
            await Clients.Caller.SendAsync("gameStart", response.Serialize());
            if (color == Player.BLACK)
            {
                await Clients.Caller.SendAsync("yourMove", GameManagerService.StartTurn(gameId, color).Serialize());
            }

            await base.OnConnectedAsync();
        }
    }
}