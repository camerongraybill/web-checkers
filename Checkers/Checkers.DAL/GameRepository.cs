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

        private static Dictionary<Guid, Game> store = new Dictionary<Guid, Game>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Game get(Guid gameId)
        {
            if (!store.ContainsKey(gameId))
            {
                store[gameId] = Game.CreateStartingGame();
            }

            return store[gameId];
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void update(Guid gameId, Game game)
        {
            store[gameId] = game;
        }

    }
}
