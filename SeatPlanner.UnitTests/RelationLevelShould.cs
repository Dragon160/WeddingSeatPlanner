using System;
using Xunit;

namespace SeatPlanner.UnitTests
{
    public class RelationLevelShould
    {
        [Fact]
        public void BeEqual()
        {
            Assert.Equal(RelationLevel.Known, RelationLevel.Known);
        }
    }
}