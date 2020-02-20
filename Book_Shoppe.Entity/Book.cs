using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.Entity
{
    public class Book
    {
        public int BookID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genere { get; set; }
        public int Price { get; set; }
        public int NoOfPages { get; set; }
    }
}
