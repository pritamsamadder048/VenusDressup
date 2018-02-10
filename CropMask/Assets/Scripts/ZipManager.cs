using System;
using System.IO;
using System.Text;
using System.IO.Compression;

public class ZipManager
{

    public delegate void ProgressDelegate(string sMessage);

    public static void Compress(FileInfo fi)
    {
        // Get the stream of the source file. 
        using (FileStream inFile = fi.OpenRead())
        {
            // Prevent compressing hidden and already compressed files. 
            if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".gz")
            {
                // Create the compressed file. 
                using (FileStream outFile = File.Create(fi.FullName + ".gz"))
                {
                    using (GZipStream Compress = new GZipStream(outFile,
                            CompressionMode.Compress))
                    {
                        // Copy the source file into the compression stream.
                        byte[] buffer = new byte[4096];
                        int numRead;
                        while ((numRead = inFile.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            Compress.Write(buffer, 0, numRead);
                        }
                        Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                            fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                    }
                }
            }
        }
    }

    public static void Decompress(FileInfo fi)
    {
        // Get the stream of the source file. 
        using (FileStream inFile = fi.OpenRead())
        {
            // Get original file extension, for example "doc" from report.doc.gz.
            string curFile = fi.FullName;
            string origName = curFile.Remove(curFile.Length - fi.Extension.Length);

            //Create the decompressed file. 
            using (FileStream outFile = File.Create(origName))
            {
                using (GZipStream Decompress = new GZipStream(inFile,
                        CompressionMode.Decompress))
                {
                    
                    //Copy the decompression stream into the output file.
                    byte[] buffer = new byte[4096];
                    int numRead;
                    while ((numRead = Decompress.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        outFile.Write(buffer, 0, numRead);
                    }
                    Console.WriteLine("Decompressed: {0}", fi.Name);

                }
            }
        }
    }




    public static void CompressFile(string sDir, string sRelativePath, GZipStream zipStream)
    {
        //Compress file name
        char[] chars = sRelativePath.ToCharArray();
        zipStream.Write(BitConverter.GetBytes(chars.Length), 0, sizeof(int));
        foreach (char c in chars)
            zipStream.Write(BitConverter.GetBytes(c), 0, sizeof(char));

        //Compress file content
        byte[] bytes = File.ReadAllBytes(Path.Combine(sDir, sRelativePath));
        zipStream.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
        zipStream.Write(bytes, 0, bytes.Length);
    }

    public static bool DecompressFile(string sDir, GZipStream zipStream, ProgressDelegate progress)
    {
        //Decompress file name
        byte[] bytes = new byte[sizeof(int)];
        int Readed = zipStream.Read(bytes, 0, sizeof(int));
        if (Readed < sizeof(int))
            return false;

        int iNameLen = BitConverter.ToInt32(bytes, 0);
        bytes = new byte[sizeof(char)];
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < iNameLen; i++)
        {
            zipStream.Read(bytes, 0, sizeof(char));
            char c = BitConverter.ToChar(bytes, 0);
            sb.Append(c);
        }
        string sFileName = sb.ToString();
        if (progress != null)
            progress(sFileName);

        //Decompress file content
        bytes = new byte[sizeof(int)];
        zipStream.Read(bytes, 0, sizeof(int));
        int iFileLen = BitConverter.ToInt32(bytes, 0);

        bytes = new byte[iFileLen];
        zipStream.Read(bytes, 0, bytes.Length);

        string sFilePath = Path.Combine(sDir, sFileName);
        string sFinalDir = Path.GetDirectoryName(sFilePath);
        if (!Directory.Exists(sFinalDir))
            Directory.CreateDirectory(sFinalDir);

        using (FileStream outFile = new FileStream(sFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            outFile.Write(bytes, 0, iFileLen);

        return true;
    }

    public static void CompressDirectory(string sInDir, string sOutFile, ProgressDelegate progress)
    {
        string[] sFiles = Directory.GetFiles(sInDir, "*.*", SearchOption.AllDirectories);
        int iDirLen = sInDir[sInDir.Length - 1] == Path.DirectorySeparatorChar ? sInDir.Length : sInDir.Length + 1;

        using (FileStream outFile = new FileStream(sOutFile, FileMode.Create, FileAccess.Write, FileShare.None))
        using (GZipStream str = new GZipStream(outFile, CompressionMode.Compress))
            foreach (string sFilePath in sFiles)
            {
                string sRelativePath = sFilePath.Substring(iDirLen);
                if (progress != null)
                    progress(sRelativePath);
                CompressFile(sInDir, sRelativePath, str);
            }
    }

    public static void DecompressToDirectory(string sCompressedFile, string sDir, ProgressDelegate progress)
    {
        using (FileStream inFile = new FileStream(sCompressedFile, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (GZipStream zipStream = new GZipStream(inFile, CompressionMode.Decompress, true))
        {
            while (DecompressFile(sDir, zipStream, progress)) ;
        }
    }

    
}


