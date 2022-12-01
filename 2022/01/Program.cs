// Task 1

Console.WriteLine(FindElfWithMostCalories());
// Task 2
Console.WriteLine(FindTopThreeElfsCalories());

static int FindElfWithMostCalories()
{
    var input = GetPuzzleInput("input.txt");
    int max = int.MinValue;
    int elfSupplyCalories = 0;
    foreach (var line in input)
    {
        if (string.IsNullOrEmpty(line))
        {
            if (elfSupplyCalories > max)
            {
                max = elfSupplyCalories;
            }
            elfSupplyCalories = 0;
        }
        else
        {
            int supplyCalories = int.Parse(line);
            elfSupplyCalories += supplyCalories;
        }
    }
    return max;
}

static int FindTopThreeElfsCalories()
{
    var input = GetPuzzleInput("input.txt");
    int elfSupplyCalories = 0;
    List<int> elfs = new();
    foreach (var line in input)
    {
        if (string.IsNullOrEmpty(line))
        {
            elfs.Add(elfSupplyCalories);
            elfSupplyCalories = 0;
        }
        else
        {
            int supplyCalories = int.Parse(line);
            elfSupplyCalories += supplyCalories;
        }
    }
    return elfs.OrderByDescending(x => x).Take(3).Sum();
}

static List<string> GetPuzzleInput(string fileName)
{
    var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
    return File.ReadAllLines(path)
        .ToList();
}