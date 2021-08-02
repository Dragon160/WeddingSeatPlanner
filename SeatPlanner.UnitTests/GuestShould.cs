namespace SeatPlanner.UnitTests
{
    using System;
    using Xunit;

    public class GuestShould
    {

        [Fact]
        public void BeEqual()
        {
            Assert.Equal(new Guest("John"), new Guest("John"));
        }
    }
}
