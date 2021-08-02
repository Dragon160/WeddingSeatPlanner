using System.Collections.Generic;
using System.Linq;

namespace SeatPlanner
{
    public static class GuestExtensions
    {
        public static IEnumerable<GuestRelation> WithFamily(this Guest guest, params string[] familyNames)
        {
            var familyMembers = new List<Guest>();
            familyMembers.AddRange(familyNames.Select(name => new Guest(name)));
            return familyMembers.SelectMany(member => GuestRelation.To(member, familyMembers.ToArray(), new RelationLevel(10))).Distinct();
        }
    }
}
