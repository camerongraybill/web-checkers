using System;
using Checkers.Models;

namespace Checkers.Services
{
    public class CheckerService
    {
        public Piece MakeChecker()
        {
            Piece c = new Piece();

            c.X = 5;
            c.Y = 0;

            return c;
        }
    }
}
