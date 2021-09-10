using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Book.Model.DAO
{
    public class Payment
    {
        public int Id { get; set; }
        public string Command { get; set; }
        public string Account { get; set; }
        public string TxnId { get; set; }
        public string TxnDate { get; set; }
        public decimal Sum { get; set; }
        public DateTime Created { get; set; }
    }
}
