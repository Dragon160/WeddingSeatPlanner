using Xunit;

namespace SeatPlanner.UnitTests
{
    public class TableShould
    {
        [Fact]
        public void SeatGuestWhenFreePlaceAvailable()
        {
            var sut = new Table("id", 4);
            Assert.True(sut.TryPlaceGuest(new Guest("Anna")));
        }

        [Fact]
        public void ReturnFalseWhenFull()
        {
            var sut = new Table("id", 1);
            sut.TryPlaceGuest(new Guest("Anna"));
            Assert.False(sut.TryPlaceGuest(new Guest("Anna")));
        }

        [Fact]
        public void ReserveSeatsForWeddingCouple()
        {
            var sut = new Table("id", 2, true);
            Assert.False(sut.TryPlaceGuest(new Guest("Anna")));
        }
    }
}