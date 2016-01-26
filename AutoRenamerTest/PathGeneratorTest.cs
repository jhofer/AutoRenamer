using AutoRenamer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class PathGeneratorTest
    {
        [TestMethod]
        public void GenerateNewPath_OtherFolder()
        {
            IPathGenerator gen = new PathGenerator();
            var renaming = gen.GenerateNewPath(
                @"C:\test - Copy - Copy\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV.avi",
                @"21 Grams (2003)\21 Grams (2003).avi", @"C:\movies");

            Assert.AreEqual(@"C:\test - Copy - Copy\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Grams (2003)\21 Grams (2003).avi",
                renaming.RenamedPath);
            Assert.AreEqual(@"C:\movies\21 Grams (2003)\21 Grams (2003).avi", renaming.NewPath);
            Assert.AreEqual(
                @"C:\test - Copy - Copy\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV.avi",
                renaming.OrginalPath);
        }


        [TestMethod]
        public void GenerateNewPath_SameFolder()
        {
            IPathGenerator gen = new PathGenerator();
            var renaming = gen.GenerateNewPath(
                @"C:\movies\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV.avi",
                @"21 Grams (2003)\21 Grams (2003).avi", @"C:\movies");

            Assert.AreEqual(@"C:\movies\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Grams (2003)\21 Grams (2003).avi", renaming.RenamedPath);
            Assert.AreEqual(@"C:\movies\21 Grams (2003)\21 Grams (2003).avi", renaming.NewPath);
            Assert.AreEqual(@"C:\movies\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV.avi",
                renaming.OrginalPath);
        }

        [TestMethod]
        public void GenerateNewPath_SubFolder()
        {
            IPathGenerator gen = new PathGenerator();
            var renaming = gen.GenerateNewPath(
                @"C:\movies\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV.avi",
                @"21 Grams (2003)\21 Grams (2003).avi", @"C:\movies\done");

            Assert.AreEqual(@"C:\movies\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Grams (2003)\21 Grams (2003).avi", renaming.RenamedPath);
            Assert.AreEqual(@"C:\movies\done\21 Grams (2003)\21 Grams (2003).avi", renaming.NewPath);
            Assert.AreEqual(@"C:\movies\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV\21 Gramm German 2003 AC3 DVDRip XViD iNTERNAL-VhV.avi",
                renaming.OrginalPath);
        }
    }
}