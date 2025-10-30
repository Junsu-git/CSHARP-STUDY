namespace Dir
{
    public class MainApp
    {
        static void Main(string[] args)
        {
            string directory;
            if ((args.Length < 1))
                directory = ".";
            else directory = args[0];

            Console.WriteLine($"{directory} directory Info");
            Console.WriteLine("- Directories :");
            var directories = (from dir in Directory.GetDirectories(directory)
                               let Info = new DirectoryInfo(dir)
                               select new
                               {
                                   Name = Info.Name,
                                   Attributes = Info.Attributes
                               }).ToList();
            foreach (var dir in directories)
                Console.WriteLine($"{dir.Name} : {dir.Attributes}");

            Console.WriteLine("- Files:");
            var files = (from file in Directory.GetFiles(directory)
                         let Info = new FileInfo(file)
                         select new
                         {
                             Name = Info.Name,
                             Size = Info.Length,
                             Attributes = Info.Attributes
                         }).ToList();
            foreach (var file in files)
                Console.WriteLine($"{file.Name} : {file.Size} bytes, {file.Attributes}");
        }
    }
}