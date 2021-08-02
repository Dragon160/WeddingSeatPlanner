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
            PlaceForRelationShipLevel(RelationLevel.Family);
            PlaceForRelationShipLevel(RelationLevel.GoodFriends);
            PlaceForRelationShipLevel(RelationLevel.Known);

            PlaceRemaining();

        }

        private void PlaceRemaining()
        {
            foreach (var remainingGuest in _guests.Where(g => g.IsSeated == false).ToArray())
            {
                PlaceInFirstFreeTable(remainingGuest);
            }
        }

        private void PlaceForRelationShipLevel(RelationLevel relationLevel)
        {
            var familyMemberRelationships = _relations.Where(rel => rel.GuestRelationship.Item3 == relationLevel).ToArray();
            foreach (var member in familyMemberRelationships.SelectMany(f => f.InvolvedGuests()))
            {
                if(member.IsSeated)
                    continue;
                
                var familyOfMember = familyMemberRelationships.Where(p => p.ContainsGuest(member))
                    .SelectMany(f => f.InvolvedGuests()).Distinct();

                var freeTableForAllMembers = _tables.FirstOrDefault(t => t.FreeSeats >= familyOfMember.Count());
                if (freeTableForAllMembers == null)
                {
                    // no splitting supported ATM
                    throw new Exception($"Cannot find a free table for family size of {familyOfMember.Count()}");
                }

                foreach (var guest in familyOfMember)
                {
                    freeTableForAllMembers.TryPlaceGuest(guest);
                }

            }
        }

        private void PlaceInFirstFreeTable(Guest guest)
        {
            var freeTable = _tables.FirstOrDefault(t => t.IsFree);
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
            var joseph = new Guest("Joseph K.");
            _relations.AddRange(joseph.WithFamily("Anne K.", "Anja K.", "Martin K.", "Hans K.", "Andrea K.").Distinct());

            var claudia = new Guest("Claudia P.");
            _relations.AddRange(claudia.WithFamily("Jens W.", "Oma", "Opa").Distinct());

            var tati = new Guest("Tatjana S.");
            _relations.AddRange(tati.WithFamily("Martin S.").Distinct());

            var wagner = new Guest("Christoph W.");
            var atif = new Guest("Atif Ö.");
            _relations.Add(GuestRelation.To(wagner, atif, RelationLevel.GoodFriends));

            var wolf = new Guest("Michi W.");
            var franz = new Guest("Franz. H.");
            var christina = new Guest("Christina W.");
            var ramona = new Guest("Ramona F.");
            _relations.Add(GuestRelation.To(wolf, franz, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(wolf, christina, RelationLevel.Family));
            _relations.Add(GuestRelation.To(franz, ramona, RelationLevel.Family));

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
