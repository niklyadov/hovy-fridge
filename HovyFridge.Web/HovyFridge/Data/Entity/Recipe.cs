namespace HovyFridge.Data.Entity
{
    public class Recipe : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
        public int? OwnerId { get; set; }
    }
}