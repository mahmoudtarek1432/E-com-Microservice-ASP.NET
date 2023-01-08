namespace Discount.API.Entites
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int  Amount { get; set; }

        //default
        public Coupon()
        {
            Amount = 0;
            Description = "Coupon Code not valid";
            ProductName = "No Discount";
        }
    }
}
