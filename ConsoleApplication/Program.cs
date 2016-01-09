using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRenamer;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            var renamer = new Renamer();
            renamer.Run();
          
            Console.WriteLine("tschüss");
            Console.ReadLine();
        }
    }
}
