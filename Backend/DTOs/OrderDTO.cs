
namespace WebApi.DTOs

{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime Requireddate { get; set; }
        public DateTime Shippeddate { get; set; }
        public string? Shipaddress { get; set; }
        public string? Shipcity { get; set; }
    }
}
