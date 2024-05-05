using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PdfMergeUtility
{
    public static class Utilities
    {
        public static IEnumerable<FileInfo> GetFilesInDirectoryRecursive(string currentDirectory, string[] extensionFilter)
        {
            var fileInfos = new List<FileInfo>();

            foreach (string filePathName in Directory
                .EnumerateFiles(currentDirectory, "*.*", SearchOption.AllDirectories))
            {
                var fileInfo = new FileInfo(filePathName);
                if (extensionFilter.Contains(fileInfo.Extension))
                {
                    fileInfos.Add(fileInfo);
                }
            }

            return fileInfos;
        }

        public static System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(',') ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");

            dllName = dllName.Replace(".", "_");

            if (dllName.EndsWith("_resources")) return null;

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(typeof(Program).Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());

            byte[] bytes = (byte[])rm.GetObject(dllName);

            return System.Reflection.Assembly.Load(bytes);
        }
    }
}
