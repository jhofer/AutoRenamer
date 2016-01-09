namespace AutoRenamer
{
    public interface IFileBot
    {
        bool Rename(string filePath, out string newFileName);
    }
}