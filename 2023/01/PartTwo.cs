namespace _01
{
    internal class PartTwo
    {
        private const string FILE_NAME = "input.txt";
        static List<string> numbersSpelledOut =
        [
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        ];

        public static int Solve()
        {
            int sum = 0;
            var input = GetPuzzleInputLines("input.txt");

            foreach (var line in input)
            {
                int firstNumber = GetFirstNumber(line);
                int lastNumber = GetLastNumber(line);

                if (firstNumber == 0)
                {
                    firstNumber = lastNumber;
                }

                if (lastNumber == 0)
                {
                    lastNumber = firstNumber;
                }

                string calibrationValueString = $"{firstNumber}{lastNumber}";
                if (int.TryParse(calibrationValueString, out int calibrationValue))
                {
                    sum += calibrationValue;
                }
            }

            return sum;
        }

        public static int GetFirstNumber(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    return int.Parse(line[i].ToString());
                }
                string stringNumber = string.Empty;
                while (char.IsLetter(line[i]))
                {
                    stringNumber += line[i].ToString();
                    if (stringNumber.Length > 2)
                    {
                        if (numbersSpelledOut.Any(c => stringNumber.Contains(c)))
                        {
                            return GetIntegerFromNumberSpelledOut(stringNumber);
                        }
                    }
                    if (line.Length - 1 == i)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (char.IsDigit(line[i]))
                {
                    return int.Parse(line[i].ToString());
                }
            }

            return 0;
        }

        public static int GetLastNumber(string line)
        {
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    return int.Parse(line[i].ToString());
                }
                string stringNumber = string.Empty;
                while (char.IsLetter(line[i]))
                {
                    stringNumber += line[i].ToString();
                    if (stringNumber.Length > 2)
                    {
                        string reversedStringNumber = ReverseString(stringNumber);
                        if (numbersSpelledOut.Any(reversedStringNumber.Contains))
                        {
                            return GetIntegerFromNumberSpelledOut(reversedStringNumber);
                        }
                    }
                    i--;
                }
                if (char.IsDigit(line[i]))
                {
                    return int.Parse(line[i].ToString());
                }
            }

            return 0;
        }

        public static string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static int GetIntegerFromNumberSpelledOut(string numberSpelledOut) => numberSpelledOut switch
        {
            var s when s.Contains("one") => 1,
            var s when s.Contains("two") => 2,
            var s when s.Contains("three") => 3,
            var s when s.Contains("four") => 4,
            var s when s.Contains("five") => 5,
            var s when s.Contains("six") => 6,
            var s when s.Contains("seven") => 7,
            var s when s.Contains("eight") => 8,
            var s when s.Contains("nine") => 9,
            _ => throw new Exception("Invalid number spelled out")
        };

        private static List<string> GetPuzzleInputLines(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.ReadAllLines(path).ToList();
        }
    }
}
