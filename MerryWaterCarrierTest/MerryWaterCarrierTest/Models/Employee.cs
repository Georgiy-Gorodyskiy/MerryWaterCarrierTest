using MerryWaterCarrierTest.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerryWaterCarrierTest.Models
{
    public class Employee
    {
        public Employee()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public GenderEnum Gender { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string DisplayName
        {
            get
            {
                return Name + " " + Patronymic + " " + Surname;
            }
        }
    }
}
