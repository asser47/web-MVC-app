using System.ComponentModel.DataAnnotations;

namespace testcoreApp.Models
{
    public class Employee
    {
        [Required(ErrorMessage ="Please enter your name")]
        [DataType(DataType.Text)]
        [StringLength(20,MinimumLength =3,ErrorMessage ="string with minium 3")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage ="Department is missing")]
        public string Department { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.Text)]
        public string JobTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
