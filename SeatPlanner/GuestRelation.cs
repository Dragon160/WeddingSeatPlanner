using System;
using System.Collections.Generic;
using System.Linq;

namespace SeatPlanner
{
    public class GuestRelation : ValueObject<GuestRelation>
    {
        public Tuple<Guest, Guest, RelationLevel> GuestRelationship { get; }

        private GuestRelation(Guest theGuest, Guest theOtherGuest, RelationLevel level)
        {
            if(theGuest == null || theOtherGuest == null)
            {
                throw new ArgumentNullException();
            }

            if(theOtherGuest == theGuest)
            {
                throw new ArgumentException("Cannot add relation between the same guest");
            }

            var guestContainer = new[] { theGuest, theOtherGuest }.OrderBy(g => g.Name).ToArray();
            GuestRelationship = new Tuple<Guest, Guest, RelationLevel>(guestContainer.First(), guestContainer.Last(), level);
        }

        public static implicit operator string(GuestRelation p)
        {
            return $"{p.GuestRelationship.Item1} with relation {p.GuestRelationship.Item3} to {p.GuestRelationship.Item2}";
        }

        public override string ToString()
        {
            return $"{GuestRelationship.Item1} with relation {GuestRelationship.Item3} to {GuestRelationship.Item2}";
        }

        public Guest[] InvolvedGuests()
        {
            return new[] {GuestRelationship.Item1, GuestRelationship.Item2};
        }

        public bool ContainsGuest(Guest guest)
        {
            return InvolvedGuests().Contains(guest);
        }

        public static GuestRelation[] To(Guest guest, Guest[] others, RelationLevel level)
        {
            return others.Where(g => g != guest).Select(otherGuest => new GuestRelation(guest, otherGuest, level)).ToArray();
        }

        public static GuestRelation To(Guest guest, Guest otherGuest, RelationLevel level)
        {
            return new(guest, otherGuest, level);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GuestRelationship;
        }
    }
}
