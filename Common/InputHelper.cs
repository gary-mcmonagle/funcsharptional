using System.Linq;
namespace Common;

public static class InputHelper
{
    public static IEnumerable<string> GetInputLines(string fileName) => File.ReadAllLines(fileName);
    public static IEnumerable<int> GetInputLinesOfInts(string fileName) =>
        File.ReadAllLines(fileName).Select(int.Parse);

}
