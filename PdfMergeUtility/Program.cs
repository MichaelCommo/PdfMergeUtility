namespace PdfMergeUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo file1Info;
            FileInfo file2Info;

            var mergeService = new PdfMergeService();
            //var tuplesOfPdfsToMerge = new List<Tuple<FileInfo, FileInfo>>();

            // get file 1 from user
            Console.WriteLine("Enter path to the file containing pages 1, 3, 5, etc...");
            var file1 = Console.ReadLine();
            try
            {
                file1Info = new FileInfo(file1);
                if (!file1Info.Exists) throw new Exception();
            }
            catch
            {
                throw new InvalidOperationException($"Could not find file {file1}. Try Again...");
            }

            // get file 2 from user
            Console.WriteLine("Enter path to the file containing pages 2, 4, 6, etc...");
            var file2 = Console.ReadLine();
            try
            {
                file2Info = new FileInfo(file2);
                if (!file2Info.Exists) throw new Exception();
            }
            catch
            {
                throw new InvalidOperationException($"Could not find file {file2}. Try Again...");
            }

            // get desitination file from user
            Console.WriteLine("Path and name to store the resulting file?");
            var targetFile = Console.ReadLine();
            var targetFileInfo = new FileInfo(targetFile);

            // merge the two pdfs
            try
            {
                mergeService.Merge(file1Info, file2Info, targetFileInfo);
                Console.WriteLine("Completed Successfully.");
                Console.WriteLine("Press any key to exit...");

            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }         

        }
    }
}
