using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Models;

namespace Checkers.Services
{
    class AvailableMoveService
    {
        public List<Move> GetMoves(Board board, Player player)
        {
            //----Get Normal Moves----
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.Get(x, y) != null)
                    {

                    }

                }

            }
        }
    }
}
