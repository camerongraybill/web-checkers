using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Checkers.DAL
{
    public class GameRepository
    {
        private static readonly GameRepository instance = new GameRepository();

        static GameRepository()
        {
        }
        private GameRepository()
        {
        }
        public static GameRepository Instance
        {
            get
            {
                return instance;
            }
        }

        private static Dictionary<Guid, Game> gameStore = new Dictionary<Guid, Game>();
        private static Dictionary<String, (Guid, Player)> userStore = new Dictionary<String, (Guid, Player)>();


        [MethodImpl(MethodImplOptions.Synchronized)]
        public Game getGame(Guid gameId)
        {
            if (!gameStore.ContainsKey(gameId))
            {
                gameStore[gameId] = Game.CreateStartingGame();
            }

            return gameStore[gameId];
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void update(Guid gameId, Game game)
        {
            gameStore[gameId] = game;
        }

        public (Guid, Player) getUser(string userId)
        {
            return userStore[userId];
        }

        public void addUser(string userId, Guid gameId, Player player)
        {
            userStore[userId] = (gameId, player);
        }

    }
}
