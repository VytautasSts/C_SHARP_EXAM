namespace CSHARP_Exam.Models
{
    public struct OrderItem
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public OrderItem(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}