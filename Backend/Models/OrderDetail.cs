namespace WebApi.Models
{
    public class OrderDetail
    {
        public int Productid { get; set; }
        public decimal Unitprice { get; set; }
        public int Qty { get; set; }
        public decimal Discount { get; set; }
    }
}
