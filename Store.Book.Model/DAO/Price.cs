using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Book.Model.DAO
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public Book Book { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
    }
}
