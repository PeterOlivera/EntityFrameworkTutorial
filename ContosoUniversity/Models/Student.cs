using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [Column("LastName")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "First character has to be upper case and the remaining characters to be alphabetical")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [Column("MiddleName")]
        [StringLength(50, ErrorMessage = "Middle name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "First character has to be upper case and the remaining characters to be alphabetical")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [Column("FirstName")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "First character has to be upper case and the remaining characters to be alphabetical")]
        public string FirstName { get; set; }

        [Display(Name = "Phone Number")]
        [Column("PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Enrollment Date")]
        [Column("EnrollmentDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{LastName} , {FirstName} {MiddleName}";
            }
        }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}