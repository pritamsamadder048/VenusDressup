using UnityEngine;
using System.Collections;
using System.IO;
using SharpCompress.Readers;
using SharpCompress.Archives.SevenZip;

public class SharpCompressExtractor 
{

   

    public static void Un7Zip(string inputPath,string extractionPath = null)
    {
        
        if(File.Exists(inputPath))
        {
            Debug.Log("Un7Zip File eist : " + inputPath);
            //if(SevenZipArchive.IsSevenZipFile(inputPath))
            //{
            //    Debug.Log(string.Format("{0} is a 7Zip File"));
            //    //using (Stream stream = File.OpenRead(inputPath))
            //    //{

            //    //    var reader = SevenZipArchive.Open(stream);
            //    //    //while (reader.MoveToNextEntry())
            //    //    //    {
            //    //    //        string dir, file;
            //    //    //        if (reader.Entry.IsDirectory)
            //    //    //        {
            //    //    //            dir = reader.Entry.Key;
            //    //    //            Debug.Log("its a directory : " + dir);
            //    //    //            continue;
            //    //    //        }
            //    //    //        if (!reader.Entry.IsDirectory)
            //    //    //        {
            //    //    //            //Debug.Log(reader.Entry.Key);
            //    //    //            //reader.WriteEntryToDirectory(@"C:\temp", new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
            //    //    //            file = reader.Entry.Key;

            //    //    //            Debug.Log("its a file : " + file);
            //    //    //            continue;
            //    //    //        }
            //    //    //    }
            //    //    reader.Dispose();
            //    //}

            //    using (SevenZipArchive sa = SevenZipArchive.Open(inputPath))
            //    {

            //    }

            //}
            //else
            //{
            //    Debug.Log(string.Format("{0} is not a 7Zip File"));
            //}

            using (SevenZipArchive sa = SevenZipArchive.Open(inputPath))
            {
                Debug.Log("seven zip archive opened , total size is : " + sa.TotalSize);

                using (SevenZipArchive.SevenZipReader reader = (SevenZipArchive.SevenZipReader)sa.ExtractAllEntries())
                {
                    //reader.WriteAllToDirectory(extractionPath, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });   //extract all file to a specific directory

                    while (reader.MoveToNextEntry())
                    {
                        string dir, file;
                        if (reader.Entry.IsDirectory)
                        {
                            dir = reader.Entry.Key;
                            Debug.Log("its a directory : " + dir);
                            string directoryName = (extractionPath != null && extractionPath != "") ? Path.Combine(extractionPath, dir) : dir;
                            if (!Directory.Exists(directoryName))
                            {
                                Debug.Log(string.Format("creating Directory : {0}", directoryName));
                                Directory.CreateDirectory(directoryName);
                            }
                            continue;
                        }
                        if (!reader.Entry.IsDirectory)
                        {
                            //Debug.Log(reader.Entry.Key);
                            //reader.WriteEntryToDirectory(@"C:\temp", new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                            file = reader.Entry.Key;
                            Debug.Log("its a file : " + file);
                            string directoryName = (extractionPath != null && extractionPath != "") ? Path.Combine(extractionPath, Path.GetDirectoryName(file)) : Path.GetDirectoryName(file);
                            if (!Directory.Exists(directoryName))
                            {
                                Debug.Log(string.Format("creating Directory Before extracting File : {0}", directoryName));
                                Directory.CreateDirectory(directoryName);
                            }
                            string fileName = (Path.GetFileName(file) != string.Empty) ? (extractionPath != null && extractionPath != "") ? Path.Combine(directoryName, Path.GetFileName(file)) : Path.GetFileName(file) : string.Empty;

                            if (fileName.Length > 0)
                            {
                                Debug.Log(string.Format("Extracting File To : {0}", fileName));


                                //reader.WriteEntryToDirectory();
                                //reader.WriteEntryTo(fileName);
                                reader.WriteEntryToFile(fileName, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });

                            }



                            continue;
                        }
                    }
                }
            }

        }
    }
}
