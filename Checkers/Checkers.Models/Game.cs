using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class Game
    {
        public Board board { get; set; }
        public Player turn;

        public static Game CreateStartingGame()
        {
            Game game = new Game
            {
                board = Board.GetStartingBoard(),
                turn = Player.BLACK
            };

            return game;
        }

    }
}
