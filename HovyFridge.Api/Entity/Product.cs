namespace HovyFridge.Api.Entity
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descriprion { get; set; }
        public string? BarCode { get; set; }
        public string? ImageName { get; set; }
        public User? Owner { get; set; }
    }
}
