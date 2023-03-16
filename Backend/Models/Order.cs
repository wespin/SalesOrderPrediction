namespace WebApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Requireddate { get; set; }
        public DateTime Shippeddate { get; set; }
        public string? Shipaddress { get; set; } 
        public string? Shipcity { get; set; }

        public int Empid { get; set; }
        public int Shipperid { get; set; }
        public string? Shipname { get; set; }
        public DateTime Orderdate { get; set; }
        public Decimal Freight { get; set; }
        public string? Shipcountry { get; set; }
        public IEnumerable<OrderDetail>? Detail { get; set; }
    }
}
