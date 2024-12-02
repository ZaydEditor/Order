using System.Text;
using System.Text.Json;

internal class Program
{
    static void Main(string[] args)
    {
        var newOrder = new Order()
        {
            OrderId = 1,
            CustomerName = "Zayd",
            TotalAmount = 10,
            Items = new List<OrderItem>
            {
                new()
                {
                    ItemName = "Something",
                    Quantity = 5,
                    Price = 200
                },
                new()
                {
                    ItemName = "Anything",
                    Quantity = 10,
                    Price = 50
                },
                new()
                {
                    ItemName = "Nothing",
                    Quantity = 3,
                    Price = 300
                }
            }
        };

        var jsonOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
        };

        Console.WriteLine($"Order id = {newOrder.OrderId}, Customer name = {newOrder.CustomerName}," +
              $" Total amount = {newOrder.TotalAmount}");
        foreach (var item in newOrder.Items)
        {
            Console.WriteLine($"Item name = {item.ItemName}, Quantity = {item.Quantity}, Price = {item.Price}");
        }

        Console.WriteLine();
        var serialized = JsonSerializer.Serialize(newOrder, jsonOptions);
        using var memoryStream = new MemoryStream();

        var bytes = Encoding.UTF8.GetBytes(serialized);
        memoryStream.Write(bytes, 0, bytes.Length);
        memoryStream.Position = 0;

        //Update order
        var json = Encoding.UTF8.GetString(memoryStream.ToArray());
        json = json.Replace("\"Quantity\": 5", "\"Quantity\": 20");
        json = json.Replace("\"ItemName\": \"Nothing\"", "\"ItemName\": \"Everything\"");

        bytes = Encoding.UTF8.GetBytes(json);
        memoryStream.Write(bytes, 0, bytes.Length);
        memoryStream.Position = 0;

        Order updatedOrder = JsonSerializer.Deserialize<Order>(json, jsonOptions);
        Console.WriteLine("Updated order: ");
        Console.WriteLine($"Order id = {updatedOrder.OrderId}, Customer name = {updatedOrder.CustomerName}," +
            $" Total amount = {updatedOrder.TotalAmount}");
        foreach (var item in updatedOrder.Items)
        {
            Console.WriteLine($"Item name = {item.ItemName}, Quantity = {item.Quantity}, Price = {item.Price}");
        }
    }
}
























public class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public int TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; }
}
public class OrderItem
{
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
