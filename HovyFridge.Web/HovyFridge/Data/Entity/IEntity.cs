namespace HovyFridge.Data.Entity
{
    public interface IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleteTimestamp { get; set; }
    }
}