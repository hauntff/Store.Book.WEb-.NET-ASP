using Store.Book.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Book.Model.DAO
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public OrderStatusEnum Status { get; set; }
        public decimal Total { get; set; }
        public DateTime Created { get; set; }
    }
}
