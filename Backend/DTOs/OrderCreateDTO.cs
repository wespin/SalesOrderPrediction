using System.Net;
using WebApi.Models;

namespace WebApi.DTOs
{
    public class OrderCreateDTO
    {
        public int Empid { get; set; }
        public int Shipperid { get; set; }
        public string? Shipname { get; set; }
        public string? Shipaddress { get; set; }
        public string? Shipcity { get; set; }
        public DateTime Shippeddate { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime Requireddate { get; set; }
        public Decimal Freight { get; set; } 
        public string? Shipcountry { get; set; }
        public IEnumerable<OrderDetail>? Detail { get; set; }
    }
}
