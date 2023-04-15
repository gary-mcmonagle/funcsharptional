Dictionary<RockPaperScissors, RockPaperScissors> RockPaperScissorsRules = new()
    {
        { RockPaperScissors.Rock, RockPaperScissors.Scissors },
        { RockPaperScissors.Paper, RockPaperScissors.Rock },
        { RockPaperScissors.Scissors, RockPaperScissors.Paper },
    };

Dictionary<RockPaperScissors, int> RockPaperScissorsScores = new()
    {
        { RockPaperScissors.Rock, 1 },
        { RockPaperScissors.Paper, 2 },
        { RockPaperScissors.Scissors, 3 },
    };


Func<Player, Dictionary<char, RockPaperScissors>> GetPaperScissorsMap = (player) => new Dictionary<char, RockPaperScissors>
{
    { player == Player.Me ? 'X' : 'A', RockPaperScissors.Rock },
    { player == Player.Me ? 'Y' : 'B', RockPaperScissors.Paper },
    { player == Player.Me ? 'Z' : 'C', RockPaperScissors.Scissors },
};

Dictionary<char, GameOutcome> GameOutcomes = new()
{
    { 'Z', GameOutcome.Win },
    { 'X', GameOutcome.Lose },
    { 'Y', GameOutcome.Draw },
};

var input = Common.InputHelper.GetInputLines("input.txt").ToList();
var maps = new Dictionary<Player, Dictionary<char, RockPaperScissors>>
{
    { Player.Me, GetPaperScissorsMap(Player.Me) },
    { Player.Oponent, GetPaperScissorsMap(Player.Oponent) },
};

var getScore = (List<Round> rounds) =>
    rounds.Aggregate(0, (acc, round) =>
    {
        var (o, m) = round;
        var scoreBonus = round switch
        {
            var (opponent, me) when opponent == me => 3,
            var (opponent, me) when RockPaperScissorsRules[me] == opponent => 6,
            _ => 0
        };
        return acc + RockPaperScissorsScores[m] + scoreBonus;
    });

Func<RockPaperScissors, GameOutcome, RockPaperScissors> GetRequiredMove = (RockPaperScissors opponentMove, GameOutcome requiredOutcome) =>
{
    if (requiredOutcome == GameOutcome.Draw)
    {
        return opponentMove;
    }
    else if (requiredOutcome == GameOutcome.Lose)
    {
        return RockPaperScissorsRules[opponentMove];
    }
    else
    {
        return RockPaperScissorsRules[RockPaperScissorsRules[opponentMove]];
    }
};


var partOneRounds = input.Select(x => x.Split(" "))
    .Select(x => new Round(
        maps[Player.Oponent][x[0][0]],
        maps[Player.Me][x[1][0]]
    )).ToList();

var partTwoRounds = input.Select(x => x.Split(" "))
    .Select(x => new Round(
        maps[Player.Oponent][x[0][0]],
        GetRequiredMove(maps[Player.Oponent][x[0][0]], GameOutcomes[x[1][0]])
    )).ToList();

Console.WriteLine($"part 1: {getScore(partOneRounds)}");
Console.WriteLine($"part 2: {getScore(partTwoRounds)}");

enum RockPaperScissors
{
    Rock,
    Paper,
    Scissors,
};

enum GameOutcome
{
    Win,
    Lose,
    Draw
};

enum Player { Me, Oponent };

record Round(RockPaperScissors Oponent, RockPaperScissors Me);
