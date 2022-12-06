using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerryWaterCarrierTest.Models
{
    public class Tag
    {
        public Tag()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
