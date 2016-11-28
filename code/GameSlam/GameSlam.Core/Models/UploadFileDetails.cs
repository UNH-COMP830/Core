using System.Collections.Generic;

namespace GameSlam.Core.Models
{
   public class UploadFileDetails
    {
        public UploadFileDetail WindowsDetails { get; set; }
        public UploadFileDetail LinuxDetails { get; set; }
        public UploadFileDetail OsxDetails { get; set; }
        public List<UploadFileDetail> Screenshots { get; set; }

        public UploadFileDetails()
        {
            Screenshots = new List<UploadFileDetail>();
        }
    }


}
