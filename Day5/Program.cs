using System.Linq;
using System.Text.RegularExpressions;
using Day5;
/*
    [C]         [Q]         [V]    
    [D]         [D] [S]     [M] [Z]
    [G]     [P] [W] [M]     [C] [G]
    [F]     [Z] [C] [D] [P] [S] [W]
[P] [L]     [C] [V] [W] [W] [H] [L]
[G] [B] [V] [R] [L] [N] [G] [P] [F]
[R] [T] [S] [S] [S] [T] [D] [L] [P]
[N] [J] [M] [L] [P] [C] [H] [Z] [R]
 1   2   3   4   5   6   7   8   9 

*/
var initalState = new List<string> {
    "NRGP",
    "JTBLFGDC",
    "MSV",
    "LSRCZP",
    "PSLVCWDQ",
    "CTNWDMS",
    "HDGWP",
    "ZLPHSCMV",
    "RPFLWGZ"
};


var stacks = initalState.Select(s => new CrateStack(s.Select(c => new Crate(c)).ToList())).ToList();

Console.WriteLine(stacks[0].Crates.Last().id);

var yard = new Shipyard(stacks);

var parse = (string s) =>
{
    Regex rg = new(@"move (\d+) from (\d+) to (\d+)");
    var matches = rg.Matches(s);
    return new Instruction(int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value), int.Parse(matches[0].Groups[3].Value));
};

var instructions = Common.InputHelper.GetInputLines("input.txt").Select(s => parse(s)).ToList();

Func<Shipyard, Instruction, bool> move = (Shipyard yard, Instruction instruction) =>
{
    for (var i = 0; i < instruction.Amount; i++)
    {
        yard.TransferSingle(yard.Stacks[instruction.From - 1], yard.Stacks[instruction.To - 1]);
    }
    return true;
};

Func<Shipyard, Instruction, bool> moveGroup = (Shipyard yard, Instruction instruction) =>
{
    yard.TransferGroup(yard.Stacks[instruction.From - 1], yard.Stacks[instruction.To - 1], instruction.Amount);
    return true;
};


instructions.ForEach(i => moveGroup(yard, i));
var lastCrates = yard.Stacks.Select(s => s.Crates.Last().id).ToList();
Console.WriteLine(string.Join("", lastCrates));

record Instruction(int Amount, int From, int To);


