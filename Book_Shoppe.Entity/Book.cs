﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.Entity
{
    [Table("Book")]
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
        public int GenreID { get; set; }


        [Required]
        public int Price { get; set; }

        [Required]
        public int NoOfPages { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("GenreID")]
        public Genre Genre { get; set; }
    }

    [Table("Genre")]
    public class Genre
    {
        [Required]
        public int GenreID { get; set; }

        [Required]
        public string GenreName { get; set; }
    }


}
