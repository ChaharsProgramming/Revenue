using System.ComponentModel.DataAnnotations.Schema;
using Revenue.AI.Entity.SeedWorks;

namespace Revenue.AI.Entity.Entities
{
    [Table("Product")]
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public long Price { get; set; }
        public int StockQuantity { get; set; }
    }
}