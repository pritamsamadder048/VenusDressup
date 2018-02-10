using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine;


class SharpUnzip
{

    public static void Unzip(string zipPath ,string extractionPath=null)
    {
        if(File.Exists(zipPath))
        {
            Debug.Log("Zip File exist trying to extract");
        }
        else
        {
            Debug.Log("Zip File does not exist");
            return;
        }
        using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipPath)))
        {

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {

                //Debug.Log(theEntry.Name);

                string directoryName =(extractionPath!=null && extractionPath !="")? Path.Combine(extractionPath, Path.GetDirectoryName(theEntry.Name)) : Path.GetDirectoryName(theEntry.Name);
                string fileName =(Path.GetFileName(theEntry.Name)!=string.Empty)? (extractionPath != null && extractionPath != "") ? Path.Combine(directoryName, Path.GetFileName(theEntry.Name)) : Path.GetFileName(theEntry.Name) :string.Empty ;

                Debug.Log(string.Format("Directory name : {0}   File name : {1}", directoryName, fileName));
                // create directory
                if (directoryName.Length > 0)
                {
                    if(!Directory.Exists(directoryName))
                    {
                        Debug.Log("Creating Directory for extraction : " + directoryName);
                        Directory.CreateDirectory(directoryName);
                    }
                }

                if (fileName != String.Empty)
                {
                    Debug.Log(string.Format("extracting file : {0}", fileName));
                    using (FileStream streamWriter = File.Create(fileName))
                    {

                        int size = 2048;
                        byte[] data = new byte[2048];
                        Debug.Log("File created : " + streamWriter.Name);
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                            streamWriter.Flush();
                            //streamWriter.Close();
                        }
                    }
                }
            }
        }
    }
}

