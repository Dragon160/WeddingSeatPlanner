using System;
using System.Collections.Generic;
using System.Linq;

namespace SeatPlanner
{
    public class Seater
    {
        private readonly List<Guest> _guests = new();
        private readonly List<GuestRelation> _relations = new();
        private readonly List<Table> _tables = new();

        public void StartSeater()
        {
            AddTables();
            AddGuestsAndRelations();
            PrintGuests();

            CalculateTablePlan();
            PrintTablePlan();
            VerifyAllAreSeated();
        }

        private void VerifyAllAreSeated()
        {
            if (_guests.Count != _tables.SelectMany(t => t.SeatedGuests).Count())
            {
                Console.WriteLine("--> VERIFICATON ERROR. Not all guests have been seated");
            }
        }

        private void CalculateTablePlan()
        {
            // prioritize all guests with relations
            foreach (var guest in _relations.SelectMany(r => r.InvolvedGuests()))
            {
                PlaceGuest(guest);
            }

            // remaining guests
            foreach (var remainingGuest in _guests.Where(g => g.IsSeated == false).ToArray())
            {
                PlaceGuest(remainingGuest);
            }
        }

        private void PlaceGuest(Guest guest)
        {
            while (guest.IsSeated == false)
            {
                var guestRelationsOrdered = _relations
                    .Where(p => p.ContainsGuest(guest))
                    .OrderBy(p => p.GuestRelationship.Item3).ToList();

                // no relation to anyone -> pick first free table
                if (guestRelationsOrdered.Count == 0)
                {
                    PlaceInFirstFreeTable(guest);
                }

                // there are relations
                foreach (var sortedRelationShip in guestRelationsOrdered)
                {
                    var theChoosenTable = _tables.FirstOrDefault(t =>
                        t.IsFree() && t.SeatedGuests.Contains(sortedRelationShip.TheOtherPerson(guest)));

                    // no table with relatives found -> pick first free table
                    if (theChoosenTable == null)
                    {
                        PlaceInFirstFreeTable(guest);
                        break;
                    }

                    // found a table with known people -> pick that one
                    if (theChoosenTable.TryPlaceGuest(guest))
                        break;
                }
            }
        }

        private void PlaceInFirstFreeTable(Guest guest)
        {
            var freeTable = _tables.FirstOrDefault(t => t.IsFree());
            if (freeTable == null)
                throw new Exception("No more free table available");

            freeTable.TryPlaceGuest(guest);
        }

        private void PrintTablePlan()
        {

            Console.WriteLine("");
            Console.WriteLine("Calculated Table Plan:");
            foreach (var table in _tables)
            {
                Console.WriteLine(table);
            }
        }

        private void AddTables()
        {
            _tables.AddRange(new[]
            {
                new Table("One", 8), 
                new Table("Two", 8),
                new Table("Three", 8),
                new Table("Four", 8),
                new Table("Five", 8),
                new Table("Six", 8),
                new Table("Seven", 8),
                new Table("Eight", 8),
                new Table("Nine", 8),
                new Table("Ten", 8),
            });
        }

        private void AddGuestsAndRelations()
        {
            var joseph = new Guest("Joseph Klein");
            _relations.AddRange(joseph.WithFamily("Anne Klein", "Anja Klein", "Martin Klein", "Hans Klein", "Andrea Klein").Distinct());

            var claudia = new Guest("Claudia Probst");
            _relations.AddRange(claudia.WithFamily("Jens Wagenführer", "Oma", "Opa").Distinct());

            var tati = new Guest("Tatjana Schulz");
            _relations.AddRange(tati.WithFamily("Martin Schulz").Distinct());

            var wagner = new Guest("Christoph Wagner");
            
            _guests.Add(wagner);
            _guests.AddRange(_relations.SelectMany(rel => rel.InvolvedGuests()).Distinct());
        }

        private void PrintGuests()
        {
            Console.WriteLine($"All Guests({_guests.Count})........");
            foreach (var guest in _guests)
            {
                Console.WriteLine(guest);
            }

            Console.WriteLine("");
            Console.WriteLine("with relations........");
            foreach (var rels in _relations)
            {
                Console.WriteLine(rels);
            }
        }
    }
}
