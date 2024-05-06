using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        [DisplayName("Reason")]
        public string reason { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public string PhoneNum { get; set; }
        [DisplayName("Transaction Id")]
        public string transactionId { get; set; }
        public string? Cancelled { get; set; } = "None";

        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        [DisplayName("Department")]
        public Department Department { get; set; }
        

    }
}
