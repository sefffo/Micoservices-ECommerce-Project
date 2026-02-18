namespace OrderApi.Domain.Entities
{
    public class Order
    {
        int id { get; set; }


        public int ProductId { get; set; }

        public int ClientId { get; set; }

        public int PurchaseQuantity { get; set; }

        public DateTime OrderDate { set; get; } = DateTime.Now;





    }
}
