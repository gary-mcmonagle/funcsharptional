Console.WriteLine("Hello World!");
var input = Common.InputHelper.GetInputLines("input.txt").ToList();
var forrest = new List<Tree>();
for (var i = 0; i < input.Count; i++)
{
    var line = input[i];
    for (var j = 0; j < line.Length; j++)
    {
        forrest.Add(new Tree(int.Parse(line[j].ToString()), j, i));
    }
}

//var visibleTrees = forrest.Where(t => IsVisible(t, forrest)).ToList();
// Console.WriteLine(visibleTrees.Count);
var scenicScore = forrest.Select(t => GetScenicScore(t, forrest)).Max();
Console.WriteLine(scenicScore);
List<(Direction, List<Tree>)> GetSightObscurers(List<Tree> forrest, Tree tree)
{
    var obscurers = new List<(Direction, List<Tree>)>();
    obscurers.Add((
        Direction.Left,
        forrest.Where(t => t.x < tree.x && t.y == tree.y).OrderByDescending(t => t.x).ToList()));
    obscurers.Add((
        Direction.Right,
        forrest.Where(t => t.x > tree.x && t.y == tree.y).OrderBy(t => t.x).ToList()));
    obscurers.Add((
        Direction.Up,
        forrest.Where(t => t.x == tree.x && t.y < tree.y).OrderByDescending(t => t.y).ToList()));
    obscurers.Add((
        Direction.Down,
        forrest.Where(t => t.x == tree.x && t.y > tree.y).OrderBy(t => t.y).ToList()));
    return obscurers;
};

bool IsEdge(Tree tree, List<Tree> forrest)
{
    var maxWidth = forrest.Max(t => t.x);
    var maxHeight = forrest.Max(t => t.y);
    return tree.x == 0 || tree.x == maxWidth || tree.y == 0 || tree.y == maxHeight;
}

bool IsVisibleInLine(Tree tree, List<Tree> column) => column.All(t => t.height < tree.height);

int AmountVisibleInLine(Tree tree, List<Tree> column, Direction direction)
{
    var count = 0;
    foreach (var t in column)
    {
        count++;
        if (t.height >= tree.height)
        {
            break;
        }
    }
    return count;
}

int GetScenicScore(Tree tree, List<Tree> forrest) =>
    GetSightObscurers(forrest, tree)
        .Select(o => AmountVisibleInLine(tree, o.Item2, o.Item1))
        .Aggregate(1, (acc, x) => acc * x);


bool IsVisible(Tree tree, List<Tree> forrest) =>
    IsEdge(tree, forrest) ||
    GetSightObscurers(forrest, tree)
        .Any(o => IsVisibleInLine(tree, o.Item2));
enum Direction
{
    Up,
    Down,
    Left,
    Right
}

record Tree(int height, int x, int y);