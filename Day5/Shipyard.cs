namespace Day5;

public class Shipyard
{
    public Shipyard(List<CrateStack> stacks)
    {
        Stacks = stacks;
    }

    public List<CrateStack> Stacks { get; set; }

    public void TransferSingle(CrateStack from, CrateStack to)
    {
        to.Add(from.Remove());
    }

    public void TransferGroup(CrateStack from, CrateStack to, int amount)
    {
        var removed = new List<Crate>();
        for (var i = 0; i < amount; i++)
        {
            removed.Add(from.Remove());
        }
        removed.Reverse();
        to.Crates.AddRange(removed);
    }
}