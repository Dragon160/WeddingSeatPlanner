using System.Collections.Generic;

namespace SeatPlanner
{
    public class Guest : ValueObject<Guest>
    {
        public string Name { get; }
        public bool IsSeated { get; private set; }
        
        public Guest(string name){
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public static implicit operator string(Guest p)
        {
            return $"{p.Name}";
        }

        public void SetSeated() => IsSeated= true;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
