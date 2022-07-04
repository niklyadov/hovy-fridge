namespace HovyFridge.Data.Entity
{
    public class FridgeAccessLevel : Entity
    {
        public long UserId { get; set; }

        public long FridgeId { get; set; }

        public bool AccessToRead { get; set; }

        public bool AccessToEditProductsList { get; set; }

        public bool AccessToEdit { get; set; }

        public bool AccessToRemove { get; set; }
    }
}