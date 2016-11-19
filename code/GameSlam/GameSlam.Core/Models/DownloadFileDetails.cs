using System.Collections.Generic;  

namespace GameSlam.Core.Models
{
    public class DownloadFileDetails
    {
        public List<FileStorageDetail> Screenshots { get; set; }
        public FileStorageDetail ProgramFile { get; set; }

        public DownloadFileDetails()
        {
            Screenshots = new List<Models.FileStorageDetail>();
        }
    }
}
