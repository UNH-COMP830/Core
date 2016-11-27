using System;
using System.Collections.Generic;
using GameSlam.Core.Enums;
using System.ComponentModel.DataAnnotations;         

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
                                                                    
        [Required]
        public Category CategoryId { get; set; }

        [Required]
        public ApprovalStatus StatusId { get; set; }

        [Required(ErrorMessage = "Valid time is Required")]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }
        
        // navigation
        public virtual ApplicationUser UserId { get; set; }
                  
    }
}
