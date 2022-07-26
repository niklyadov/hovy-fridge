namespace HovyFridge.Entity
{
    public class ProductSuggestion : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}