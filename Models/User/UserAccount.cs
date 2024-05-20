using Subscription_based_marketing.Enums;
using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;

namespace Subscription_based_marketing.Models.User
{
    public class UserAccount : Entity
    {
        [Key]

        public Guid UserID { get; set; }

        [Required]

        public string UserName { get; set; }

        public string UserEmailAddress { get; set; }


        public long PhoneNumber { get; set; }

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
