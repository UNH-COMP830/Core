using System;
using GameSlam.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSlam.Core.Models
{
    public class GameDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public String Title { get; set; }

        [Required]
        [StringLength(1024)]
        public String Description { get; set; }
                                       
        [Required(ErrorMessage = "Valid time is Required")]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }

        [Required]
        [StringLength(128)]
        public String BlogStorageGuidId { get; set; }

        [Required]
        [StringLength(128)]
        public String UserId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // navigation     
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }   
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }   
        [ForeignKey("StatusId")]
        public virtual ApprovalStatus Status { get; set; }

    }
}
