using System;
using System.Diagnostics;

namespace SeatPlanner
{
    public record RelationLevel: IComparable
    {
        public int Level { get; init; }
        private RelationLevel(int level)
        {
            if (level is > 3 or < 0)
            {
                throw new ArgumentOutOfRangeException($"{level} not between 0 and 3)");
            }
            Level = level;
        }

        public static RelationLevel Family => new (3);
        public static RelationLevel GoodFriends => new (2);
        public static RelationLevel Known => new (1);

        public override string ToString()
        {
            return Level switch
            {
                1 => "Known",
                2 => "GoodFriends",
                3 => "Family",
                _ => ""
            };
        }

        public static implicit operator string(RelationLevel p)
        {
            return p.Level switch
            {
                1 => "Known",
                2 => "GoodFriends",
                3 => "Family",
                _ => ""
            };
        }

        public int CompareTo(object obj)
        {
            return Level;
        }
    }
}
