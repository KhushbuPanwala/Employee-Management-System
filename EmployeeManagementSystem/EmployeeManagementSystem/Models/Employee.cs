using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[^<>'~!@#$%^&*()_+|\"]+$", ErrorMessage = "Special Character not allow")]
        public string Name { get; set; }

        [Required]
        [StringLength(70)]
        [RegularExpression("^[^<>'~!@#$%^&*()_+|\"]+$", ErrorMessage = "Special Character not allow.")]
        public string Surname { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Qualification { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Display(Name = "Contact No.")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public int DeptId { get; set; }
        [Display(Name = "Department")]
        public Department Department { get; set; }

    }
}
