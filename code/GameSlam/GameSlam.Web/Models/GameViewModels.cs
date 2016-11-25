using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameSlam.Core.Extentions;
using GameSlam.Core.Enums;

namespace GameSlam.Web.Models
{
    public class UploadGameViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]  
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [FileExtensions2("jpg",
              ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        [Display(Name = "ScreenShot1")]
        public HttpPostedFileBase ScreenShot1 { get; set; }

        [Required]
        [FileExtensions2("jpg",
              ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        [Display(Name = "ScreenShot2")]
        public HttpPostedFileBase ScreenShot2 { get; set; }

        [Required]
        [FileExtensions2("jpg",
              ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        [Display(Name = "ScreenShot3")]
        public HttpPostedFileBase ScreenShot3 { get; set; }

        [Required]
        [FileExtensions2("jpg",
              ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        [Display(Name = "ScreenShot4")]
        public HttpPostedFileBase ScreenShot4 { get; set; }

        [Required]
        [FileExtensions2("jpg",
              ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        [Display(Name = "ScreenShot5")]
        public HttpPostedFileBase ScreenShot5 { get; set; }

        [Required]
        [FileExtensions2("jpg",
              ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        [Display(Name = "ScreenShot6")]
        public HttpPostedFileBase ScreenShot6 { get; set; }


        [Required]
        [Display(Name = "download-windows")]
        public HttpPostedFileBase DownloadWindows { get; set; }

        [Required]
        [Display(Name = "download-linux")]
        public HttpPostedFileBase DownloadLinux { get; set; }

        [Required]
        [Display(Name = "download-osx")]
        public HttpPostedFileBase Downloadosx { get; set; }
             
        [Required]
        [Display(Name = "Category")]
        public CategoryEnum CategoryId { get; set; }
    }

}