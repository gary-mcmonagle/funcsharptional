using System.Linq;
using System.Text.RegularExpressions;
using DaySeven;

var root = new Dir { Name = "root" };
var isOutput = (string line) => line.StartsWith("$");
var isChangeDir = (string line) => line.Contains("$ cd");
var getChangeDir = (string line) => new Regex("\\$ cd (.*)").Matches(line)[0].Groups[1].Value;
var isDirOutput = (string line) => line.Contains("dir");
var getFileOutput = (string line) =>
{
    var groups = new Regex("(\\d+) (.*)").Matches(line)[0].Groups;
    return new Tuple<int, string>(int.Parse(groups[1].Value), groups[2].Value);
};
var getDirOutput = (string line) => line.Replace("dir ", "").Trim();

var classify = (string line) => line switch
{
    string s when isOutput(s) && !isChangeDir(s) => OutputCategory.LS,
    string s when isChangeDir(s) => OutputCategory.ChangeDir,
    string s when isDirOutput(s) => OutputCategory.Dir,
    _ => OutputCategory.File,
};

var input = Common.InputHelper.GetInputLines("input.txt").ToList();

var currentDir = root;
for (var i = 1; i < input.Count; i++)
{
    var category = classify(input[i]);
    switch (category)
    {
        case OutputCategory.ChangeDir:
            var dirName = getChangeDir(input[i]);

            currentDir = dirName == ".." ?
            currentDir.Parent! : currentDir.Directories.First(x => x.Name == dirName);
            break;
        case OutputCategory.Dir:
            var dir = new Dir { Name = getDirOutput(input[i]) };
            currentDir.AddDirectory(dir);
            break;
        case OutputCategory.File:
            var file = getFileOutput(input[i]);

            currentDir.AddFile(new DaySeven.File { Name = file.Item2, Size = file.Item1 });
            break;
        case OutputCategory.LS:
            break;
    }


}

var partOne = root.GetAllChildren().Where(x => x.Size() <= 100000).Select(x => x.Size()).Sum();
Console.WriteLine(partOne);

var usedSpace = 70000000 - root.Size();
var requiredSpace = 30000000;
var spaceToFree = requiredSpace - usedSpace;
var partTwo = root.GetAllChildren()
    .Where(x => x.Size() >= spaceToFree)
    .OrderBy(x => x.Size()).First().Size();
Console.WriteLine(partTwo);
enum OutputCategory
{
    Dir,
    File,
    ChangeDir,
    LS,
}
