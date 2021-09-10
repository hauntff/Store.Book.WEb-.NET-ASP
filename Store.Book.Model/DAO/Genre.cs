using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Book.Model
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public Genre Parent { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
