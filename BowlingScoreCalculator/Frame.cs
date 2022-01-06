namespace BowlingScoreCalculator
{
    public class Frame
    {
        private List<char> _rolls;
        private bool _isLast { get; set; }

        public Frame(bool isLast = false)
        {
            _rolls = new List<char>();
            _isLast = isLast;
        }

        public void AddRoll(char roll)
        {
            _rolls.Add(roll);
        }

        public int GetRollsCount()
        {
            return _rolls.Count;
        }

        public int GetRollScoreByIndex(int index)
        {
            int lastRoll = 0;
            if (index > 0)
            {
                lastRoll = GetRollScore(_rolls[index - 1]);
            }

            return GetRollScore(_rolls[index], lastRoll);
        }

        public int GetScore(int nextRoll1 = 0, int nextRoll2 = 0)
        {
            if (_isLast)
            {
                return GetScoreForLast();
            }

            int result = 0;
            foreach (char r in _rolls)
            {
                if (r == BowlingConsts.None)
                {
                    continue;
                }

                if (r == BowlingConsts.Strike)
                {
                    return 10 + nextRoll1 + nextRoll2;
                }

                if (r == BowlingConsts.Spare)
                {
                    return 10 + nextRoll1;
                }

                result += int.Parse(r.ToString());
            }

            return result;
        }

        private int GetScoreForLast()
        {
            if (_rolls.Count == 3 && _rolls[0] == BowlingConsts.Strike)
            {
                int second = GetRollScore(_rolls[1]);
                return 10 + second + GetRollScore(_rolls[2], second);
            }

            if (_rolls.Count == 3 && _rolls[1] == BowlingConsts.Spare)
            {
                return 10 + GetRollScore(_rolls[2]);
            }

            int first = GetRollScore(_rolls[0]);
            return first + GetRollScore(_rolls[1], first);
        }

        private int GetRollScore(char roll, int lastRollValue = 0)
        {
            if (roll == BowlingConsts.None)
            {
                return 0;
            }

            if (roll == BowlingConsts.Strike)
            {
                return 10;
            }

            if (roll == BowlingConsts.Spare)
            {
                return 10 - lastRollValue;
            }

            return int.Parse(roll.ToString());
        }
    }
}
