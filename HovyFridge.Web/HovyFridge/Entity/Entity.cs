namespace HovyFridge.Entity
{
    public abstract class Entity : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
    }
}