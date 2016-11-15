﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSlam.Core.Models
{
    public class PublicResponse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Rating { get; set; }
                                             
        [Required]                      
        public virtual ApplicationUser UserId { get; set; }

        [StringLength(1024)]
        public String Content { get; set; }

        [Required (ErrorMessage ="Valid time is Required")]
        public DateTime? CreateTime { get; set; }


        //navigate                  
        public virtual GameDetail GameInfo { get; set; }
    }
}
