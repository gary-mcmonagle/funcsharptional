namespace Day5;
public class CrateStack
{
    public CrateStack(List<Crate> crates)
    {
        Crates = crates;
    }

    public List<Crate> Crates { get; set; }

    public void Add(Crate crate)
    {
        Crates.Add(crate);
    }

    public Crate Remove()
    {
        var tail = Crates.Last();
        Crates.RemoveAt(Crates.Count - 1);
        return tail;
    }
}