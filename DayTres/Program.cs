var alpla = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
var parseLine = (string line) => new Rucksack(
        line.Substring(0, (int)(line.Length / 2)),
        line.Substring((int)(line.Length / 2), (int)(line.Length / 2))
    );
var input = Common.InputHelper.GetInput("input.txt", parseLine).ToList();
Func<Rucksack, char> getMisplaced = (Rucksack rucksack) =>
{
    var (comp1, comp2) = rucksack;
    return comp1.ToCharArray()
        .Where(c => comp2.Contains(c))
        .FirstOrDefault();
};

Func<List<string>, char> getCommon = (List<string> rucksacks) =>
{
    var common = rucksacks[0].ToCharArray();
    for (int i = 1; i < rucksacks.Count; i++)
    {
        common = common.Where(c => rucksacks[i].Contains(c)).ToArray();
    }

    return common.FirstOrDefault();
};

Func<char, int> GetMisplacedValue = (char c) =>
{
    var isUpper = char.IsUpper(c);
    var index = alpla.IndexOf(c.ToString().ToLower()) + 1;
    return isUpper ? index + 26 : index;
};

Console.WriteLine($"PART 1: {input.Select(getMisplaced).Select(GetMisplacedValue).Sum()}");
var raw = Common.InputHelper.GetInputLines("input.txt").ToList();
Console.WriteLine($"PART 2: {raw
.Select((x, i) =>
{
    if (i != 0 && i % 3 != 0) { return new List<string>(); }

    return new List<string> { x, raw[i + 1], raw[i + 2] };
})
.Where(x => x.Count > 0)
.Select(getCommon)
.Select(GetMisplacedValue)
.Sum()}");


record Rucksack(string comp1, string comp2);

