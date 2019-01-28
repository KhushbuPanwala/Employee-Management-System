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

        
        //[StringLength(50)]
        [Display(Name = "Department Name")]
        //[Required(ErrorMessage = "Department Name is required")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        //[RegularExpression("^[^<>'\"]+$", ErrorMessage = "Special Character Don't allowance.")]
        public string DeptName { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
