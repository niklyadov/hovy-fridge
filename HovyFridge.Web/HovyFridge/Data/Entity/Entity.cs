namespace HovyFridge.Data.Entity
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleteTimestamp { get; set; }
    }
}