using Bookstore.Models;

namespace Bookstore.Services;

public class OrderState
{
    public Order Order { get; set; } = new();

    public event Action? OnChange;

    public OrderState()
    {
        Order.OrderItems = new List<OrderItem>();
    }

    public void AddItem(OrderItem item)
    {
        Order.OrderItems.Add(item);
        OnChange?.Invoke();
    }

    public void RemoveItem(OrderItem item)
    {
        Order.OrderItems.Remove(item);
        OnChange?.Invoke();
    }

    public void ResetOrder() //after placing an order
    {
        Order = new Order();
    }

}
