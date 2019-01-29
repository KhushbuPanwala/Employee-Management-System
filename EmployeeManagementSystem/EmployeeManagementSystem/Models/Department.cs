using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }
        [Display(Name = "Employees Name")]
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
