using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadImagemR2.Models
{
    public class DownloadResult
    {
        public byte[] Bytes { get; set; } = [];
        public string ContentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}