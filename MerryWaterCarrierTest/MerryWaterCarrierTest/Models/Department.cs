using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerryWaterCarrierTest.Models
{
    public class Department
    {
        public Department()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Leader")]
        public int? LeaderId { get; set; }
        public Employee? Leader { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
