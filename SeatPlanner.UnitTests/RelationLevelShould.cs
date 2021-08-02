using System;
using Xunit;

namespace SeatPlanner.UnitTests
{
    public class RelationLevelShould
    {
        [Fact]
        public void BeEqual()
        {
            Assert.Equal(new RelationLevel(2), new RelationLevel(2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        public void ThrowOnInvalidLevels(int lvl)
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=> new RelationLevel(lvl));
        }
    }
}