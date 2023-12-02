namespace _01
{
    public class PartOne
    {
        private const string FILE_NAME = "input.txt";
        public static int Solve()
        {
            int sum = 0;
            var input = GetPuzzleInputLines(FILE_NAME);
            foreach (var line in input)
            {
                List<int> numbers = new();
                foreach (char c in line)
                {
                    if (int.TryParse(c.ToString(), out int number))
                    {
                        numbers.Add(number);
                    }
                }

                if (numbers.Count > 0)
                {
                    string calibrationValueString = numbers.First().ToString() + numbers.Last().ToString();
                    if (int.TryParse(calibrationValueString, out int calibrationValue))
                    {
                        sum += calibrationValue;
                    }
                }
            }

            return sum;
        }

        private static List<string> GetPuzzleInputLines(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.ReadAllLines(path).ToList();
        }
    }
}
