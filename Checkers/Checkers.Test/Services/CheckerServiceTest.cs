using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Checkers.Test.Services
{
    public class CheckerServiceTest
    {
        [Fact]
        public void MakeCheckerTest()
        {
            var s = new CheckerService();

            var c = s.MakeChecker();

            Assert.True(c is Checker);
        }
    }
}
