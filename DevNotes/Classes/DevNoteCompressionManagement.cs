using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNotes.Classes
{
    public static class DevNoteCompressionManagement
    {
        public static byte[]? Compress(byte[]? data)
        {
            if (data != null)
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var gzip = new GZipStream(outputStream, CompressionMode.Compress))
                    {
                        gzip.Write(data, 0, data.Length);
                    }
                    return outputStream.ToArray();
                }
            }
            else
            {
                return null;
            }
        }

        public static byte[]? Decompress(byte[]? compressedData)
        {
            if (compressedData != null)
            {
                using (var inputStream = new MemoryStream(compressedData))
                using (var gzip = new GZipStream(inputStream, CompressionMode.Decompress))
                using (var outputStream = new MemoryStream())
                {
                    gzip.CopyTo(outputStream);
                    return outputStream.ToArray();
                }
            }
            else 
            { 
                return null; 
            }
        }
    }
}
