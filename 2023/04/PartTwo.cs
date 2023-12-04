namespace _04
{
    public class PartTwo
    {
        private const string FILE_NAME = "input.txt";
        public static Dictionary<int, int> CopiesOfWinningCupons = new();
        public static int Solve()
        {
            int score = 0;
            var input = GetPuzzleInputLines(FILE_NAME);
            for (int i = 1; i <= input.Count; i++)
            {
                CopiesOfWinningCupons.Add(i, 1);
            }
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
                var copiesOfCupons = CopiesOfWinningCupons[game.Id];
                for (int j = 0; j < copiesOfCupons; j++)
                {
                    for (int i = game.Id + 1; i < game.Id + localPoints + 1; i++)
                    {
                        if (CopiesOfWinningCupons.TryGetValue(i, out int value))
                        {
                            var x = value + 1;
                            CopiesOfWinningCupons[i] = x;
                        }
                    }
                }
            }
            foreach (var item in CopiesOfWinningCupons)
            {
                score += item.Value;
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
            public List<int> WinningNumbers { get; set; } = new List<int>();
            public List<int> Numbers { get; set; } = new List<int>();
        }

        private static List<string> GetPuzzleInputLines(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.ReadAllLines(path).ToList();
        }
    }
}
