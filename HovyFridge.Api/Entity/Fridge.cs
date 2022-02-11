using HovyFridge.Api.Entity.Common;

namespace HovyFridge.Api.Entity
{
    public class Fridge : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<User>? AllowedUsers { get; set; }
        public User? Owner { get; set; }
        public List <Product>? Products { get; set; }
    }
}
