using HovyFridge.Api.Entity.Common;

namespace HovyFridge.Api.Entity
{
    public class ProductHistory : IEntity
    {
        public int Id { get; set; }
        public ProductHistoryOperation ProductHistoryOperation { get; set; }
        //public User? User { get; set; }
        public Product? Product { get; set; }
        public Fridge? Fridge { get; set; }
        public DateTime? OperationDate { get; set; }
    }
}
