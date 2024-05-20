using System.ComponentModel.DataAnnotations;

namespace Subscription_based_marketing.DTO
{
    public class UserDto
    {
        public Guid UserID { get; set; }


        public string UserName { get; set; }


        [DataType(DataType.EmailAddress)]
       
        public string UserEmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
      
        public long PhoneNumber { get; set; }

        [DataType(DataType.Password)]
      
        public string UserPassword { get; set; }

        
        public string FirstName { get; set; }
      
        public string LastName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public DateTime AccountCreationDate { get; set; }

        public DateTime UserLastLoginDate { get; set; }

        public bool SubscriptionStatus { get; set; } 
    }
}
