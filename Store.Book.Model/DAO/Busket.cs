using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Book.Model.DAO
{
    public class Busket
    {
        [Key]
        public int Id { get; set; }
        public Order Order { get; set; }
        public Book Book { get; set; }
        public Price Price{ get; set; }
    }
}
