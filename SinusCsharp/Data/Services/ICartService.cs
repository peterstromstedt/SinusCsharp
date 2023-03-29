using Microsoft.CodeAnalysis.CSharp.Syntax;
using SinusCsharp.Models;

namespace SinusCsharp.Data.Services
{
    public interface ICartService
    {
        List<Cart> AddProductToCart(List<Cart> cartList, Cart cart);
        List<Cart> UpdateQuantityOfAProduct(List<Cart> cartList, Cart cart);

    }
}
