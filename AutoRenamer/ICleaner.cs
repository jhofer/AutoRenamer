using System.Collections.Generic;

namespace AutoRenamer
{
    public interface ICleaner
    {
         void Cleanup();
        void Cleanup(Renaming renaming);
    }
}