using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_07
{
    internal class Directory
    {
        public Directory? Parent { get; set; }
        public HashSet<Directory> Children { get; } = new();
        public string Name { get; set; }
        public Dictionary<string, int> Files { get; } = new();
        public int TotalFileSize
        {
            get
            {
                return Files.Sum(f => f.Value);
            }
        }
        public int TotalChildSize
        {
            get
            {
                return Children.Sum(c => c.GetSize());
            }
        }
        public Directory(string name)
        {
            Name = name;
        }

        public int GetSize()
        {
            int result = 0;
            foreach ((string name, int size) in Files)
            {
                result += size;
            }
            foreach (Directory directory in Children)
            {
                result += directory.GetSize();
            }
            return result;
        }

        public void PrintContents()
        {
            Console.WriteLine($"Printing directory.  Name: {Name}, File count: {Files.Count}, Child count: {Children.Count}");
            foreach ((string n, int s) in Files)
            {
                Console.WriteLine($"\t{n,-10}\t{s}");
            }
            foreach(Directory d in Children)
            {
                Console.WriteLine($"\tChild: {d.Name,-10}\t{d.GetSize()}");
            }
        }

        public void PrintContentsCompact()
        {
            Console.WriteLine($"Directory: {Name}, {Files.Count} Files worth {TotalFileSize} bytes.  {Children.Count} Children worth {TotalChildSize} bytes.");
        }
    }
}
