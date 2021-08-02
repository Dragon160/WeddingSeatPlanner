namespace SeatPlanner.UnitTests
{
    using System;
    using Xunit;

    public class GuestRelationShould
    {
        [Fact]
        public void BeEqualIgnoreGuestOrder()
        {
            var guest1 = new Guest("Anna");
            var guest2 = new Guest("Joseph");

            var guestRelation1 = GuestRelation.To(guest1, guest2, new RelationLevel(10));
            var guestRelation2 = GuestRelation.To(guest2, guest1, new RelationLevel(10));

            Assert.Equal(guestRelation1,guestRelation2);
        }

        [Fact]
        public void BeEqualSameOrder()
        {
            var guest1 = new Guest("Anna");
            var guest2 = new Guest("Joseph");


            Assert.Equal(GuestRelation.To(guest1, guest2, new RelationLevel(10)), GuestRelation.To(guest1, guest2, new RelationLevel(10)));
        }

        [Fact]
        public void ThrowOnRelationBetweenSame()
        {
            var guest1 = new Guest("Anna");
            var guest2 = new Guest("Anna");
            
            Assert.Throws<ArgumentException>(()=> GuestRelation.To(guest1, guest2, new RelationLevel(10)));
        }

        [Fact]
        public void ThrowWhenNoGuests()
        {
            Assert.Throws<ArgumentNullException>(() => GuestRelation.To(null, (Guest)null, new RelationLevel(10)));
        }
    }
}
