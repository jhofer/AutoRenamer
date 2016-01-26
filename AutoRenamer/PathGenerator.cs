
namespace AutoRenamer
{

    public class Renaming
    {
        public string OrginalPath { get; set; }
        public string RenamedPath { get; set; }
        public string NewPath { get; set; }

        public override string ToString()
        {
            return $"OrginalPath: {OrginalPath} RenamedPath: {RenamedPath} NewPath {NewPath}";
        }
    }

    public class PathGenerator : IPathGenerator
    {

        

        public Renaming GenerateNewPath(string originalPath, string newFileName, string destinationPath)
        {
            var index = originalPath.LastIndexOf('\\');
            var root = originalPath.Substring(0, index);
            var renamedPath = root + "\\" + newFileName;
            var newFilePath = destinationPath +"\\"+ newFileName;
            return new Renaming()
                   {
                       OrginalPath = originalPath,
                       NewPath = newFilePath,
                       RenamedPath = renamedPath,
                   };

        }

    }
}
