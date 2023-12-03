namespace _03
{
    public class PartOne
    {
        private const string FILE_NAME = "input.txt";
        public record Symbol(char Character, int X, int Y);
        public record Number(int Value, int X, int Y);

        public record EngineSchematic
        {
            public List<Symbol> Symbols { get; init; } = new();
            public List<Number> Numbers { get; init; } = new();
        }

        public static int Solve()
        {
            List<Number> validNumbers = new();
            List<string> list = GetPuzzleInputLines(FILE_NAME);
            var engine = ParseEngineSchematic(list);

            foreach (var number in engine.Numbers)
            {
                bool isValid = CheckIfNumberHasAdjacentSymbol(number, engine.Symbols);
                if (isValid)
                {
                    validNumbers.Add(number);
                }
            }

            return validNumbers.Sum(x => x.Value);
        }

        public static bool CheckIfNumberHasAdjacentSymbol(Number number, List<Symbol> symbols)
        {
            var possibleCoordinates = FindPossibleCorrdiantes(number);
            if (possibleCoordinates.Any(c => symbols.Any(s => s.X == c.Item1 && s.Y == c.Item2)))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Number: {number.Value} cord: ({number.X}, {number.Y}) has no adjacent symbols");
                return false;
            }
        }

        public static List<(int, int)> FindPossibleCorrdiantes(Number number)
        {
            if (number.Y == 122)
            {
                Console.WriteLine();
            }
            List<(int, int)> coordinates = new();
            int numberLength = number.Value.ToString().Length;
            int x = number.X;
            int y = number.Y;

            // Up
            for (int i = 0; i < numberLength + 2; i++)
            {
                coordinates.Add((x - 1, y - 1));
                x++;
            }

            x = number.X;

            // Down
            for (int i = 0; i < numberLength + 2; i++)
            {
                coordinates.Add((x - 1, y + 1));
                x++;
            }

            x = number.X;

            // Left
            coordinates.Add((x - 1, y));

            // Right
            coordinates.Add((x + numberLength, y));

            if (number.Value == 882 || number.Value == 919)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine($"Number: {number}, Number lenght: {numberLength}, Possible cords: {coordinates.Count}");
                foreach (var coordinate in coordinates)
                {
                    Console.WriteLine($"X: {coordinate.Item1}, Y: {coordinate.Item2}");
                }
                Console.WriteLine("-------------------------------------");
            }


            return coordinates;
        }


        public static EngineSchematic ParseEngineSchematic(List<string> lines)
        {
            var engineSchematic = new EngineSchematic();
            for (int i = 0; i < lines.Count; i++)
            {
                bool shouldAddOne = false;
                if (i == 130)
                {
                    Console.WriteLine();
                }
                string? line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    char character = line[j];
                    if (character == '.')
                    {
                        continue;
                    }
                    else if (!char.IsDigit(character))
                    {
                        engineSchematic.Symbols.Add(new Symbol(character, j, i));
                        continue;
                    }
                    else
                    {
                        string number = string.Empty;
                        while (char.IsDigit(line[j]))
                        {
                            bool isLastDigit = j == line.Length - 1;
                            number += line[j];
                            if (number == "882" || number == "919")
                            {
                                Console.WriteLine($"Number: {number}, X: {j - number.Length + 1}, Y: {i}");
                            }
                            if (isLastDigit)
                            {
                                shouldAddOne = true;
                                break;
                            }
                            else
                            {
                                j++;
                            }
                        }
                        if (!char.IsDigit(line[j]) && line[j] != '.')
                        {
                            engineSchematic.Symbols.Add(new Symbol(line[j], j, i));
                        }
                        if (shouldAddOne)
                        {
                            j++;
                        }
                        engineSchematic.Numbers.Add(new Number(int.Parse(number), j - number.Length, i));
                    }
                }
            }

            foreach (var symbol in engineSchematic.Symbols)
            {
                Console.WriteLine($"Symbol: {symbol.Character}, X: {symbol.X}, Y: {symbol.Y}");
            }

            return engineSchematic;
        }

        private static List<string> GetPuzzleInputLines(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.ReadAllLines(path).ToList();
        }
    }
}
