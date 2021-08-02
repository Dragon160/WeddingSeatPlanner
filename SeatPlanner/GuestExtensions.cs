using System.Collections.Generic;
using System.Linq;

namespace SeatPlanner
{
    public static class GuestExtensions
    {
        public static IEnumerable<GuestRelation> WithFamily(this Guest guest, params string[] familyNames)
        {
            var familyMembers = familyNames.Select(name => new Guest(name)).ToList();
            familyMembers.Add(guest);
            return familyMembers
                .SelectMany(member => GuestRelation.To(member, familyMembers.ToArray(),RelationLevel.Family));
        }
    }
}
