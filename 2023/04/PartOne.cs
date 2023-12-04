namespace _04
{
    public class PartOne
    {
        private const string FILE_NAME = "input.txt";
        public static int Solve()
        {
            int score = 0;
            var input = GetPuzzleInputLines(FILE_NAME);
            foreach (var line in input)
            {
                int localPoints = 0;
                var game = ParseGame(line);
                foreach (var number in game.Numbers)
                {
                    bool containsAll = game.Numbers.All(s => game.WinningNumbers.Contains(s));
                    if (game.WinningNumbers.Contains(number))
                    {
                        localPoints++;
                    }
                }
                if (localPoints != 0)
                {
                    score += CalculateScore(localPoints);
                }
                Console.WriteLine($"{game.Id}: {CalculateScore(localPoints)}");
            }

            return score;
        }

        public static int CalculateScore(int points)
        {
            int score = 1;
            for (int i = 1; i < points; i++)
            {
                score *= 2;
            }
            return score;
        }

        public static Game ParseGame(string line)
        {
            var words = line
                .Split(' ')
                .Where(x => x != "")
                .ToList();

            var game = new Game()
            {
                Id = int.Parse(words[1].Replace(":", ""))
            };

            var numbers = words.Skip(2);
            foreach (var word in numbers)
            {
                if (word == "|")
                {
                    break;
                }
                else
                {
                    game.WinningNumbers.Add(int.Parse(word));
                }
            }

            foreach (var word in numbers.Skip(game.WinningNumbers.Count + 1))
            {
                game.Numbers.Add(int.Parse(word));
            }

            return game;
        }

        public record Game
        {
            public int Id { get; set; }
            public List<int> WinningNumbers {get; set;} = new List<int>();
            public List<int> Numbers {get; set;} = new List<int>();
        }

        private static List<string> GetPuzzleInputLines(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.ReadAllLines(path).ToList();
        }
    }
}
