namespace BowlingScoreCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var scoreCalculator = new ScoreCalculator();

            int result = scoreCalculator.Calculate(args[0]);

            Console.WriteLine(result);
        }
    }
}