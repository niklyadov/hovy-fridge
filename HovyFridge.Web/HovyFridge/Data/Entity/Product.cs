using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HovyFridge.Data.Entity
{
    public class Product : Entity
    {
        [JsonIgnore]
        public Fridge? Fridge { get; set; }
        public long? FridgeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public short Amount { get; set; } = 100;
        public Unit UnitType { get; set; } = Unit.Weight;
        public int UnitValue { get; set; } = 0;
        [NotMapped]
        public int UnitCurrentValue => UnitValue * Amount / 100;
        public long? CreatedTimestamp { get; set; }
        public long? LastEditedTimestamp { get; set; }
        public long? OwnerId { get; set; }
    }
}