using System.Linq;

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

    public class GuestExtensionShould
    {
        [Fact]
        public void CreateFamily()
        {
            var guest1 = new Guest("John");
            var result = guest1.WithFamily("Hans", "Peter");

            Assert.Equal(3, result.Count());
        }
    }
}
