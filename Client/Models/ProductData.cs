using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Models
{
    public class ProductData
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Value { get; set; }
        public int Amount { get; set; }
        public string ErrorCode { get; set; }
    }
}
