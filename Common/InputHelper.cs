using System.Linq;
namespace Common;

public static class InputHelper
{

    public static string GetInputLine(string fileName) => File.ReadAllLines(fileName).First();
    public static IEnumerable<string> GetInputLines(string fileName) => File.ReadAllLines(fileName);
    public static IEnumerable<int> GetInputLinesOfInts(string fileName) =>
        File.ReadAllLines(fileName).Select(int.Parse);

    public static IEnumerable<T> GetInput<T>(string fileName, Func<string, int, T> parseLine) => File.ReadAllLines(fileName).Select((x, i) => parseLine(x, i));
    public static IEnumerable<T> GetInput<T>(string fileName, Func<string, T> parseLine) => File.ReadAllLines(fileName).Select((x, i) => parseLine(x));

}
