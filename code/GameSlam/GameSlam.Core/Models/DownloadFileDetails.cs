using System.Collections.Generic;  

namespace GameSlam.Core.Models
{
    public class DownloadFileDetails
    {
        public List<FileStorageDetail> Screenshots { get; set; }
        public FileStorageDetail windowsProgramFile { get; set; }
        public FileStorageDetail linuxProgramFile { get; set; }
        public FileStorageDetail osxProgramFile { get; set; }

        public DownloadFileDetails()
        {
            Screenshots = new List<Models.FileStorageDetail>();
        }
    }
}
