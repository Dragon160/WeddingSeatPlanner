using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatPlanner
{
    public class Table
    {
        public int Seats { get; }
        public List<Guest> SeatedGuests;
        public bool IsFree => FreeSeats > 0;
        public int FreeSeats => Seats-(SeatedGuests.Count);
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
            desc.AppendLine($"------Table {p.Id}-----------");
            foreach (var guest in p.SeatedGuests)
            {
                desc.AppendLine($" {guest}  ");
            }
            desc.AppendLine($"------Free ({p.FreeSeats})---");
            desc.AppendLine();

            return desc.ToString();
        }

        public override string ToString()
        {
            var desc = new StringBuilder();
            desc.AppendLine($"------Table {Id}-----------");
            foreach (var guest in SeatedGuests)
            {
                desc.AppendLine($" {guest}  ");
            }
            desc.AppendLine($"------Free ({FreeSeats})---");
            desc.AppendLine();

            return desc.ToString();
        }
    }
}
