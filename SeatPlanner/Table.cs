using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatPlanner
{
    public class Table
    {
        public int Seats { get; }
        public List<Guest> SeatedGuests;
        private int freeSeats => Seats- (SeatedGuests.Count);
        public string Id { get; }

        public Table(string id, int seats, bool shallReserveTwoForWeddingCouple = false)
        {
            Seats = shallReserveTwoForWeddingCouple ? seats-2 : seats;
            SeatedGuests = new();
            Id = id;
        }

        private void SeatGuest(Guest guest)
        {
            SeatedGuests.Add(guest);
            guest.SetSeated();
        }

        public void TryToPlaceGuestWithNeighbours(Guest guest, params GuestRelation[] allRelations)
        {
            if(SeatedGuests.Count >= Seats)
                return;

            var guestAndNearNeighours = allRelations
                                        .Where(rel => rel.ContainsGuest(guest))
                                        .OrderBy(rel => rel.GuestRelationship.Item3)
                                        .SelectMany(rel => rel.InvolvedGuests())
                                        .Where(guest => guest.IsSeated == false)
                                        .Take(freeSeats);

            foreach(var guy in guestAndNearNeighours)
            {
                SeatGuest(guy);
            }
        }

        public override string ToString()
        {
            var desc = new StringBuilder();
            desc.AppendLine($"------Table {Id}-------");
            SeatedGuests.Select(guest => desc.AppendLine(guest.ToString()));
            desc.AppendLine("------------------------");
            desc.AppendLine();

            return desc.ToString();
        }
    }
}
