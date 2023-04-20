var input = Common.InputHelper.GetInputLine("input.txt");
var offset = 14;
Func<string, bool> isDifferent = (string s) => s.ToCharArray().ToList().Distinct().Count() == offset;
var min = input.ToArray().ToList()
    .Select((c, i) => new Tuple<string, int>(input.Substring(i, i + offset < input.Length ? offset : 0), i))
    .Where(t => isDifferent(t.Item1))
    .Select(t => t.Item2)
    .Min() + offset;