using System;
using Checkers.Models;

namespace Checkers.Services
{
    public class CheckerService
    {
        public Checker MakeChecker()
        {
            Checker c = new Checker();

            c.X = 5;
            c.Y = 0;

            return c;
        }
    }
}
