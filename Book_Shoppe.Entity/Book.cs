﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.Entity
{
    public class Book
    {
        [Required]
        public int BookID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [MaxLength(55)]
        public string Title { get; set; }

        [Required]
        [MaxLength(26)]
        public string Author { get; set; }

        [Required]
        [MaxLength(20)]
        public string Genere { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int NoOfPages { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
