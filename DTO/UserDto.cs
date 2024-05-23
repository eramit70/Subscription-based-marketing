using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Subscription_based_marketing.DTO
{
    public class UserDto
    {
        public Guid UserID { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string UserEmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Mobile Number")]
        public long PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string UserPassword { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string State { get; set; }

        public string City { get; set; }
        [DisplayName("Account Creation Date")]
        public DateTime AccountCreationDate { get; set; }
        [DisplayName("Last Login Date")]
        public DateTime UserLastLoginDate { get; set; }
      
        public bool SubscriptionStatus { get; set; } 
    }
}
