using System.Security;

namespace Advent_Of_Code_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Directory home = new("/");
            Directory currentDirectory = home;
            HashSet<Directory> Directories = new()
            {
                home
            };

            bool collectingData = true;

            // first input puts us in home directory
            string input = GetInput("Terminal string:");

            while (collectingData)
            {
                input = GetInput("Terminal string:");

                if (input == "")
                {
                    break;
                }

                string[] parsedInput = input.Split(' ');

                if (parsedInput[0] == "$" && parsedInput[1] == "cd")
                {
                    if (parsedInput[2] == "..")
                    {
                        if (currentDirectory.Parent != null)
                        {
                            currentDirectory = currentDirectory.Parent;
                        }
                    }
                    else
                    {
                        var newDirectory = new Directory(parsedInput[2]);
                        Directories.Add(newDirectory);

                        if (currentDirectory != null)
                        {
                            currentDirectory.Children.Add(newDirectory);
                        }
                        newDirectory.Parent = currentDirectory;
                        currentDirectory = newDirectory;
                    }
                }

                if (int.TryParse(parsedInput[0], out int num))
                {
                    currentDirectory!.Files[parsedInput[1]] = num;
                }
            }

            Console.Clear();
            int result = 0;
            const int threshold = 100000;
            var finalListing = Directories.OrderBy(d => d.Name);
            foreach (Directory d in finalListing)
            {
                if (d.GetSize() <= threshold)
                {
                    result += d.GetSize();
                }
                d.PrintContentsCompact();
            }
            Console.WriteLine($"total of items <= {threshold}: {result}");
        }

        static string GetInput(string prompt)
        {
            bool validResult = false;
            string? input = null;
            while (!validResult)
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                if (input != null)
                {
                    validResult = true;
                }
            }

            return input!;
        }
    }
}