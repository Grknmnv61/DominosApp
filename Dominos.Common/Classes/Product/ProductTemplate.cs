namespace Dominos.Common.Classes
{
    public class ProductTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string ProductTypeName { get; set; }
        public int Count { get; set; }
    }
}
