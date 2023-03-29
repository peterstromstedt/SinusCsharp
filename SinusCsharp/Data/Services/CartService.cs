using SinusCsharp.Models;

namespace SinusCsharp.Data.Services
{
    public class CartService : ICartService
    {
        public List<Cart> AddProductToCart(List<Cart> cartList, Cart cart)
        {
            var item = cartList.FirstOrDefault(i => i.ProductId == cart.ProductId);
            if(item == null)
            {
                cartList.Add(cart);
            }
            else
            {
                var itemList = cartList.Where(i => i.ProductId != cart.ProductId).ToList();
                item.Quantity++;
                itemList.Add(item);
            }

            return cartList;
        }

        public List<Cart> UpdateQuantityOfAProduct(List<Cart> cartList, Cart cart)
        {
            var item = cartList.FirstOrDefault(i => i.ProductId == cart.ProductId);
            item.Quantity = cart.Quantity;
            cartList.RemoveAll(i => i.ProductId == cart.ProductId);
            cartList.Add(item);
            return cartList;
        }
    }
}
