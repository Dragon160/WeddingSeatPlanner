using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatPlanner
{
    public class Table
    {
        public int Seats { get; }
        public List<Guest> SeatedGuests;
        private int FreeSeats => Seats-(SeatedGuests.Count);
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

        public bool IsFree() => FreeSeats > 0;

        public bool TryPlaceGuest(Guest guest)
        {
            if(FreeSeats < 1)
                return false;

            SeatGuest(guest);
            return true;
        }

        public static implicit operator string(Table p)
        {
            var desc = new StringBuilder();
            desc.AppendLine($"------Table {p.Id}-------");
            foreach (var guest in p.SeatedGuests)
            {
                desc.AppendLine(guest);
            }
            desc.AppendLine("------------------------");
            desc.AppendLine();

            return desc.ToString();
        }

        public override string ToString()
        {
            var desc = new StringBuilder();
            desc.AppendLine($"------Table {Id}-------");
            foreach (var guest in SeatedGuests)
            {
                desc.AppendLine(guest);
            }
            desc.AppendLine("------------------------");
            desc.AppendLine();

            return desc.ToString();
        }
    }
}
