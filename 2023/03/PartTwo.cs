namespace _03
{
    internal class PartTwo
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
            var gears = engine.Symbols.Where(s => s.Character == '*').ToList();
            foreach (var gear in gears)
            {
                (bool isValid, int number1, int number2) = CheckIfGearHasTwoAdjacentNumbers(gear, engine.Numbers);
                if (isValid)
                {
                }
            }
            return 0;
        }

        public static List<(int, int)> GetAllNumberCoordinates()
        {
            throw new NotImplementedException();
        }

        public static (bool isValid, int number1, int number2) CheckIfGearHasTwoAdjacentNumbers(Symbol gear, List<Number> numbers)
        {
            throw new NotImplementedException();
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
