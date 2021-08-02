namespace SeatPlanner.UnitTests
{
    using Xunit;

    public class GuestShould
    {
        [Fact]
        public void BeEqual()
        {
            Assert.Equal(new Guest("John"), new Guest("John"));
        }

        [Fact]
        public void NotBeEqualOnDifferentName()
        {
            Assert.NotEqual(new Guest("Anna"), new Guest("John"));
        }
    }
}
