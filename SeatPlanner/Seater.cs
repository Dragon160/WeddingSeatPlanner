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
            //PrintGuests();

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
                new Table("One", 8, true), 
                new Table("Two", 8, true),
                new Table("Three", 8, true),
                new Table("Four", 8),
                new Table("Five", 8),
                new Table("Six", 8),
                new Table("Seven", 8),
                new Table("Eight", 8),
                new Table("Nine", 8),
                new Table("Ten", 8),
                new Table("Eleven", 8),
                new Table("Twelve", 8),
            });
        }

        private void AddGuestsAndRelations()
        {
            var josef = new Guest("Josef K.");
            _relations.AddRange(josef.WithFamily("Anne K.", "Anja K.", "Martin K.", "Hans K.", "Andrea K.", "Jürgen R", "Mathilde R.").Distinct());

            var claudia = new Guest("Claudia P.");
            _relations.AddRange(claudia.WithFamily("Jens W.", "Oma", "Opa", "Thomas M", "Steffi").Distinct());

            var tati = new Guest("Tatjana S.");
            _relations.AddRange(tati.WithFamily("Martin S.").Distinct());

            var thomas = new Guest("Thomas P.");
            _relations.AddRange(thomas.WithFamily("Asha C.").Distinct());

            var julia = new Guest("Julia S");
            _relations.AddRange(julia.WithFamily("Torsten S.").Distinct());

            var max = new Guest("Max M");
            _relations.AddRange(max.WithFamily("Anika M.").Distinct());

            var gerry = new Guest("Gerry");
            _relations.AddRange(gerry.WithFamily("Petra").Distinct());

            var mimi = new Guest("Jemima");
            _relations.AddRange(mimi.WithFamily("Andre", "Madita", "Luisa", "Jonas", "Becci", "Levi").Distinct());

            var bocki = new Guest("Christoph M");
            _relations.AddRange(bocki.WithFamily("Andi M").Distinct());

            var wolf = new Guest("Michael W");
            _relations.AddRange(wolf.WithFamily("Christina W").Distinct());

            var franz = new Guest("Franz");
            _relations.AddRange(franz.WithFamily("Ramona F.").Distinct());

            var barbara = new Guest("Barbara R");
            _relations.AddRange(barbara.WithFamily("Michi S").Distinct());

            var alex = new Guest("Alex B");
            _relations.AddRange(alex.WithFamily("Eva A", "Benni D").Distinct());

            var chris = new Guest("Chris B");
            _relations.AddRange(chris.WithFamily("Steffi A").Distinct());

            var sonja = new Guest("Sonja K");
            _relations.AddRange(sonja.WithFamily("Cornelia R").Distinct());

            var Kilian = new Guest("Kili R");
            _relations.AddRange(Kilian.WithFamily("Lena R").Distinct());

            var andi = new Guest("Andreas R");
            _relations.AddRange(andi.WithFamily("Helen B").Distinct());
            
            var sms = new Guest("SMS");
            _relations.AddRange(sms.WithFamily("Anna K", "Ched", "Micha", "Dahu", "Flori").Distinct());

            var susi = new Guest("Susi");
            var wagner = new Guest("Christoph W.");
            var atif = new Guest("Atif");
            var marcella = new Guest("marcella");
            _relations.Add(GuestRelation.To(gerry, wolf, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(marcella, julia, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(andi, franz, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(Kilian, wolf, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(Kilian, andi, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(alex, chris, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(wolf, franz, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(bocki, franz, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(bocki, wolf, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(gerry, franz, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(wagner, sms, RelationLevel.GoodFriends));
            _relations.Add(GuestRelation.To(wagner, atif, RelationLevel.Family));
            _relations.Add(GuestRelation.To(barbara, susi, RelationLevel.Family));
            _relations.Add(GuestRelation.To(max, franz, RelationLevel.Known));
            _relations.Add(GuestRelation.To(max, wolf, RelationLevel.Known));

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
