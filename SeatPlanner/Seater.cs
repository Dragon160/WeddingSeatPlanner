using System;
using System.Collections.Generic;
using System.Linq;

namespace SeatPlanner
{
    public class Seater
    {
        private List<Guest> Guests = new();
        private List<GuestRelation> Relations = new();

        private List<Table> Tables = new();

        public void StartSeater()
        {
            AddTables();
            AddGuestsAndRelations();
            PrintGuests();

            CalculateTablePlan();
            PrintTablePlan();

        }

        private void CalculateTablePlan()
        {
            while (Guests.Any(guest => guest.IsSeated == false))
            {
                var nextGuest = Guests.First(guest => guest.IsSeated == false);
                Console.WriteLine($"Try to find place for {nextGuest}");
                foreach (var table in Tables)
                {
                    table.TryToPlaceGuestWithNeighbours(nextGuest, Relations.ToArray());
                }
            }
        }

        private void PrintTablePlan()
        {

            Console.WriteLine("");
            Console.WriteLine("Calculated Table Plan:");
            foreach (var table in Tables)
            {
                Console.WriteLine(table);
            }
        }

        private void AddTables()
        {
            Tables.AddRange(new[] { new Table("One", 5), new Table("Two", 5) });
        }

        private void AddGuestsAndRelations()
        {
            var Joseph = new Guest("Joseph Klein");
            Relations.AddRange(Joseph.WithFamily("Anne Klein", "Anja Klein", "Martin Klein", "Hans Klein", "Andrea Klein").Distinct());

            var Claudia = new Guest("Claudia Probst");
            Relations.AddRange(Claudia.WithFamily("Jens Wagenführer", "Oma", "Opa").Distinct());

            var christophWagner = new Guest("Christoph Wagner");
            Guests.AddRange(Relations.SelectMany(rel => rel.Guests).Distinct());
        }

        private void PrintGuests()
        {
            Console.WriteLine($"All Guests({Guests.Count})........");
            foreach (var guest in Guests)
            {
                Console.WriteLine(guest);
            }

            Console.WriteLine("");
            Console.WriteLine("with relations........");
            foreach (var rels in Relations)
            {
                Console.WriteLine(rels);
            }
        }
    }
}
