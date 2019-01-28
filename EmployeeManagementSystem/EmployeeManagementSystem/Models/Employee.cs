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
        public string Name { get; set; }
        [Required]
        [StringLength(70)]
        public string Surname { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        
        [StringLength(20)]
        public string Qualification { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "Contact No.")]
        public string ContactNumber { get; set; }

        [Display(Name = "Department Name")]
        public int DeptId { get; set; }
        [Display(Name = "Department")]
        public Department Department { get; set; }

    }
}
