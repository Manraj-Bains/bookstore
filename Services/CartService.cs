using Bookstore.Models;

namespace Bookstore.Services;

public class CartItem
{
    public Book Book { get; set; } = null!;
    public int Quantity { get; set; }
}

public class CartService
{
    public decimal WalletBalance { get; private set; } = 100.00m;
    public List<CartItem> Items { get; } = [];

    public void AddToCart(Book book, int quantity)
    {
        var existingItem = Items.FirstOrDefault(i => i.Book.Id == book.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            Items.Add(new CartItem { Book = book, Quantity = quantity });
        }
    }

    public void RemoveFromCart(int bookId)
    {
        Items.RemoveAll(i => i.Book.Id == bookId);
    }

    public void UpdateQuantity(int bookId, int quantity)
    {
        var item = Items.FirstOrDefault(i => i.Book.Id == bookId);
        if (item != null)
        {
            if (quantity <= 0)
            {
                RemoveFromCart(bookId);
            }
            else
            {
                item.Quantity = quantity;
            }
        }
    }

    public void ClearCart()
    {
        Items.Clear();
    }

    public void Checkout()
    {
        var total = GetCartTotal();
        if (total > WalletBalance)
        {
            throw new InvalidOperationException("Insufficient funds");
        }
        WalletBalance -= total;
        ClearCart();
    }

    public void AddFunds(decimal amount)
    {
        if (amount > 0)
        {
            WalletBalance += amount;
        }
    }

    public decimal GetCartTotal()
    {
        return Items.Sum(i => i.Book.Price * i.Quantity);
    }
}
