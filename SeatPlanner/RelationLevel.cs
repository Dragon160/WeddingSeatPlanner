using System;

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
        public static implicit operator string(RelationLevel p)
        {
            return $"{p.Level}";
        }

        public static RelationLevel Family => new (3);
        public static RelationLevel GoodFriends => new (2);
        public static RelationLevel Known => new (1);
        public static RelationLevel Foreigner => new (0);

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
