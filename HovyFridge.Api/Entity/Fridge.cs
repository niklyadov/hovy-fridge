using HovyFridge.Api.Entity.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HovyFridge.Api.Entity
{
    public class Fridge : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Product> Products { get; set; } 
            = new List <Product> ();


        [NotMapped]
        public bool IsFavorite { get; set; }

        [NotMapped]
        public int ProductsCount { get; set; }

        //public List<User>? AllowedUsers { get; set; }
        //public User? Owner { get; set; }

        //[JsonIgnore]
        //public List<User>? FavoriteInUsers { get; set; }
    }
}
