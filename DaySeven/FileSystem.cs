namespace DaySeven;

public class Dir
{
    public required string Name { get; init; }
    public List<File> Files { get; set; } = new();
    public List<Dir> Directories { get; set; } = new();

    public Dir? Parent { get; set; }

    public bool IsRoot => Parent == null;

    public void AddFile(File file)
    {
        Files.Add(file);
    }

    public void AddDirectory(Dir dir)
    {
        Directories.Add(dir);
        dir.Parent = this;
    }

    public int Size()
    {
        var size = Files.Sum(f => f.Size);
        foreach (var dir in Directories)
        {
            size += dir.Size();
        }
        return size;
    }

    public List<Dir> GetAllChildren()
    {
        var children = Directories.ToList();
        foreach (var dir in Directories)
        {
            children.AddRange(dir.GetAllChildren());
        }
        return children;
    }

}

public class File
{
    public required string Name { get; init; }
    public required int Size { get; init; }
}


