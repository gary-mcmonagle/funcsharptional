Func<string, int?> parseLine = str => string.IsNullOrEmpty(str) ? null : int.Parse(str);
List<int?> input = Common.InputHelper.GetInputLines("input.txt")
    .Select(parseLine).ToList();

Func<List<int?>, List<List<int>>> GetGroups = (input) =>
{
    List<List<int>> result = new List<List<int>>();
    List<int> currentGroup = new List<int>();
    foreach (int? line in input)
    {
        if (line == null)
        {
            result.Add(currentGroup);
            currentGroup = new List<int>();
        }
        else
        {
            currentGroup.Add(line.Value);
        }
    }
    result.Add(currentGroup);
    return result;
};

var sum = GetGroups(input)
    .Select(group => group.Sum())
    .OrderByDescending(x => x)
    .Take(3)
    .Sum();

Console.WriteLine(sum);