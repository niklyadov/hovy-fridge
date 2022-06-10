using System.ComponentModel.DataAnnotations.Schema;

namespace HovyFridge.Api.Data.Entity
{
    public class ShoppingList : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        [NotMapped]
        public int ProductsAmount { get; set; }
        public int? OwnerId { get; set; }
    }
}