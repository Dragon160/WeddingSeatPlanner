using System;
using Xunit;

namespace SeatPlanner.UnitTests
{
    public class TableShould
    {
        [Fact]
        public void SeatGuestWhenFreePlaceAvailable()
        {
            var sut = new Table("id", 4);
            sut.PlaceGuestOnTable(new Guest("Anna"));

            Assert.Equal(3, sut.FreeSeats);
        }

        [Fact]
        public void ThrowWhenFull()
        {
            var sut = new Table("id", 1);
            sut.PlaceGuestOnTable(new Guest("Hans"));
            Assert.Throws<Exception>(()=> sut.PlaceGuestOnTable(new Guest("Anna")));
        }

        [Fact]
        public void ReserveSeatsForWeddingCouple()
        {
            var sut = new Table("id", 2, true);
            Assert.Throws<Exception>(()=> sut.PlaceGuestOnTable(new Guest("Anna")));
        }

        [Fact]
        public void ThrowsWhenPlacingAnAlreadySeatedGuest()
        {
            var sut = new Table("id", 2, true);
            Assert.Throws<Exception>(() =>
            {
                var guest = new Guest("Anna");
                guest.SetSeated();
                sut.PlaceGuestOnTable(guest);
            });
        }
    }
}