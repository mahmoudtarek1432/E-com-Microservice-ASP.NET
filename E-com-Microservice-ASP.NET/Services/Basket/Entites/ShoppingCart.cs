namespace Basket.API.Entites
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; }

        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            this.UserName = username;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                Items.ForEach(i => totalprice += i.Price * i.Quantity);
                return totalprice;
            }
        }
    }
}
