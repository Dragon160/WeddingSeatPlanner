using System;
using System.Collections.Generic;
using System.Linq;

namespace SeatPlanner
{
    public class GuestRelation : ValueObject<GuestRelation>
    {
        public Guest[] Guests {get;}
        public RelationLevel Level {get;}

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

            Guests = new List<Guest>{ theGuest, theOtherGuest}.OrderBy(g => g.Name).ToArray();
            Level = level;
        }

        public static implicit operator string(GuestRelation p)
        {
            return $"{p.Guests.First()} with relation {p.Level} to {p.Guests.Last()}";
        }

        public override string ToString()
        {
            return $"{Guests.First()} with relation {Level} to {Guests.Last()}";
        }

        public bool ContainsGuest(Guest guest)
        {
            return Guests.Contains(guest);
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
            yield return Guests;
            yield return Level;
        }
    }
}
