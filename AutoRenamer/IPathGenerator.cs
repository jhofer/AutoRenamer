namespace AutoRenamer
{
    public interface IPathGenerator
    {
        Renaming GenerateNewPath(string originalPath, string newFileName, string destinationPath);
    }
}