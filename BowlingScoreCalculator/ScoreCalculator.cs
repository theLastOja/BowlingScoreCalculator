namespace BowlingScoreCalculator
{
    public class ScoreCalculator
    {
        private Frame[] _frames = new Frame[BowlingConsts.NumberOfFrames];
        private const int lastFrameIndex = BowlingConsts.NumberOfFrames - 1;

        public int Calculate(string line)
        {
            char[] stringRolls =
                line
                .Replace(" ", string.Empty)
                .ToCharArray();

            int frameCounter = 0;
            foreach (char singleRoll in stringRolls)
            {
                if (_frames[frameCounter] == null)
                {
                    _frames[frameCounter] = new Frame(frameCounter == lastFrameIndex);
                }

                if (singleRoll == BowlingConsts.Strike && frameCounter != lastFrameIndex)
                {
                    _frames[frameCounter].AddRoll(singleRoll);
                    frameCounter++;
                    continue;
                }

                if (singleRoll == BowlingConsts.Strike && frameCounter == lastFrameIndex)
                {
                    _frames[frameCounter].AddRoll(singleRoll);
                    continue;
                }

                _frames[frameCounter].AddRoll(singleRoll);

                if (_frames[frameCounter].GetRollsCount() == 2 && frameCounter != lastFrameIndex)
                {
                    frameCounter++;
                }
            }

            return SumFrameScores();
        }

        private int SumFrameScores()
        {
            int result = 0;
            for (int i = 0; i < _frames.Length; i++)
            {
                if (i == lastFrameIndex)
                {
                    result += _frames[i].GetScore();
                    continue;
                }

                int nextIndex = i + 1;
                int nextRollIndex = 0;
                int nextRoll1 = _frames[nextIndex].GetRollScoreByIndex(nextRollIndex++);

                if (_frames[nextIndex].GetRollsCount() == 1)
                {
                    nextIndex++;
                    nextRollIndex = 0;
                }

                int nextRoll2 = _frames[nextIndex].GetRollScoreByIndex(nextRollIndex);

                result += _frames[i].GetScore(nextRoll1, nextRoll2);
            }

            return result;
        }
    }
}
