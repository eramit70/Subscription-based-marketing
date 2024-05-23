using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Subscription_based_marketing.DTO
{
    public class AdminDto
    {
        public Guid AdminId { get; set; }
        [DisplayName("Admin UserName")]
        public string AdminUserName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("LastName")]
        public string LastName { get; set; }

        [DisplayName("Email Address")]
        public string AdminEmail { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }

        [DisplayName("Account Creation Date")]
        public DateTime AdminAccountCreationDate { get; set; }

        [DisplayName("Last Login Date")]
        public DateTime AdminLastLoginDate { get; set; }
    }
}
