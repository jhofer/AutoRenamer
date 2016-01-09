using System.Collections.Generic;

namespace AutoRenamer
{
    public interface IFileSearcher
    {
        /// <summary>
        /// Returns all empty dirs in the given directory
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        List<string> GetEmptyDirs(string root);

        /// <summary>
        /// Returns all Files in the given directory and subdirectories with the matching extensions
        /// </summary>
        /// <param name="root"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        List<string> GetFilePaths(string root, List<string> extensions);
    }
}