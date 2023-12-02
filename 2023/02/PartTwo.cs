namespace _02
{
    internal class PartTwo
    {
        private const string FILE_NAME = "input.txt";

        public static int Solve()
        {
            int answer = 0;
            var input = GetPuzzleInputLines(FILE_NAME);
            foreach (var line in input)
            {
                int maxRedCubes = 0;
                int maxGreenCubes = 0;
                int maxBlueCubes = 0;

                var game = ParseGame(line);

                foreach (var set in game.Sets)
                {
                    if (set.Red > maxRedCubes)
                    {
                        maxRedCubes = set.Red;
                    }
                    if (set.Green > maxGreenCubes)
                    {
                        maxGreenCubes = set.Green;
                    }
                    if (set.Blue > maxBlueCubes)
                    {
                        maxBlueCubes = set.Blue;
                    }
                }
                answer += maxRedCubes * maxGreenCubes * maxBlueCubes;
            }
            return answer;
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
