namespace _02
{
    public class PartOne
    {
        private const string FILE_NAME = "input.txt";

        private static readonly int redLimit = 12;
        private static readonly int greenLimit = 13;
        private static readonly int blueLimit = 14;
        public static int Solve()
        {
            int sumOfIdsOfValidGames = 0;
            var input = GetPuzzleInputLines(FILE_NAME);
            foreach (var line in input)
            {
                var game = ParseGame(line);
                bool isGameValid = true;
                foreach (var set in game.Sets)
                {
                    if (set.Red > redLimit || set.Green > greenLimit || set.Blue > blueLimit)
                    {
                        isGameValid = false;
                        break;
                    }
                }
                if (isGameValid)
                {
                    sumOfIdsOfValidGames += game.Number;
                }
            }
            return sumOfIdsOfValidGames;
        }

        public static Game ParseGame(string line)
        {
            var words = line.Split(' ');
            var game = new Game()
            {
                Number = int.Parse(words[1].Replace(":", ""))
            };

            var set = new Set();
            for (int i = 0; i < words.Length; i++)
            {
                string? word = words[i];
                if (word.Contains("red"))
                {
                    set.Red = int.Parse(words[i - 1]);
                }
                else if (word.Contains("green"))
                {
                    set.Green = int.Parse(words[i - 1]);
                }
                else if (word.Contains("blue"))
                {
                    set.Blue = int.Parse(words[i - 1]);
                }

                if (word.Contains(';'))
                {
                    game.Sets.Add(set);
                    set = new Set();
                }

                if (words.Length - 1 == i)
                {
                    game.Sets.Add(set);
                }
            }
            return game;
        }

        public record Game
        {
            public int Number { get; set; }
            public List<Set> Sets { get; set; } = [];
        }

        public record Set
        {
            public int Red { get; set; }
            public int Green { get; set; }
            public int Blue { get; set; }
        }

        private static List<string> GetPuzzleInputLines(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.ReadAllLines(path).ToList();
        }
    }
}
