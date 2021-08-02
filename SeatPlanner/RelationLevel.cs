using System;

namespace SeatPlanner
{
    public record RelationLevel: IComparable
    {
        public int Level { get; init; }
        public RelationLevel(int level)
        {
            if (level is > 10 or < 1)
            {
                throw new ArgumentOutOfRangeException($"{level} not between 1 and 10)");
            }
            Level = level;
        }
        public static implicit operator string(RelationLevel p)
        {
            return $"{p.Level}";
        }

        public override string ToString()
        {
            return $"{Level}";
        }

        public int CompareTo(object obj)
        {
            return Level;
        }
    }
}
