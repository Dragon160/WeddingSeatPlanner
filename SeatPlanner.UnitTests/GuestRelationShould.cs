namespace SeatPlanner.UnitTests
{
    using System;
    using Xunit;

    public class GuestRelationShould
    {
        [Fact]
        public void ReturnIfContainsGuest()
        {
            var guest1 = new Guest("Anna");
            var guest2 = new Guest("Joseph");
            var sut = GuestRelation.To(guest2, guest1, new RelationLevel(10));

            Assert.True(sut.ContainsGuest(guest1));
        }

        [Fact]
        public void ReturnInvolvedGuests()
        {
            var guest1 = new Guest("Anna");
            var guest2 = new Guest("Joseph");
            var sut = GuestRelation.To(guest2, guest1, new RelationLevel(10));

            Assert.Equal(new[]{guest1, guest2}, sut.InvolvedGuests());
        }

        [Fact]
        public void BeEqualIgnoreGuestOrder()
        {
            var guest1 = new Guest("Anna");
            var guest2 = new Guest("Joseph");

            var sut1 = GuestRelation.To(guest1, guest2, new RelationLevel(10));
            var sut2 = GuestRelation.To(guest2, guest1, new RelationLevel(10));

            Assert.Equal(sut1,sut2);
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
