using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerryWaterCarrierTest.Models
{
    public class Order
    {
        public Order()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public Employee? Employee { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
