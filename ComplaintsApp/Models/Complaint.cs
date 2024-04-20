using System.ComponentModel.DataAnnotations;

namespace ComplaintsApp.Models
{
    public class Complaint
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your mobile number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid mobile number.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Please enter complaint details.")]
        [StringLength(500, ErrorMessage = "Complaint details cannot exceed 500 characters.")]
        public string ComplaintDetails { get; set; }

        public string IPAddress { get; set; }
        public string CreatedDate { get; set; }
    }
}
