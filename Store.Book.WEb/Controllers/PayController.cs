using Store.Book.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace Store.Book.WEb.Controllers
{
    public class PayController : ApiController
    {
        [HttpGet]
        public string Command(PaymentDTO payment)
        {
            var xmlString = new XDocument("result",
                new XElement("txn_id","0"),
                new XElement("result", "0"),
                new XElement("comment", "OK")).ToString();
            return xmlString;
        }
    }
}
