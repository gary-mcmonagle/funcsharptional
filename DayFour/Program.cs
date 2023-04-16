Func<string, List<int>> GetAssignment = (string line) =>
{
    var start = line.Split("-")[0];
    var end = line.Split("-")[1];
    return Enumerable.Range(int.Parse(start), int.Parse(end) - int.Parse(start) + 1).ToList();
};
Func<string, Tuple<List<int>, List<int>>> ParseLine = (line) => new Tuple<List<int>, List<int>>(
    GetAssignment(line.Split(",")[0]),
    GetAssignment(line.Split(",")[1])
);

Func<Tuple<List<int>, List<int>>, bool> FullyOverlapping = (Tuple<List<int>, List<int>> assignment) =>
{
    var ordered = new List<List<int>> { assignment.Item1, assignment.Item2 }.OrderBy(x => x.Count).ToList();
    return ordered[0].All(x => ordered[1].Contains(x));
};
Func<Tuple<List<int>, List<int>>, bool> AnyOverlap = (Tuple<List<int>, List<int>> assignment) =>
{
    var ordered = new List<List<int>> { assignment.Item1, assignment.Item2 }.OrderBy(x => x.Count).ToList();
    return ordered[0].Any(x => ordered[1].Contains(x));
};
var input = Common.InputHelper.GetInput("input.txt", ParseLine).ToList();
Console.WriteLine($@"part 1: {input.Where(x => FullyOverlapping(x)).ToList().Count}");
Console.WriteLine($@"part 2: {input.Where(x => AnyOverlap(x)).ToList().Count}");