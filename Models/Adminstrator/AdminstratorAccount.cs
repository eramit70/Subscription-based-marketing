using System.ComponentModel.DataAnnotations;
using URF.Core.EF.Trackable;

namespace Subscription_based_marketing.Models.Adminstrator
{
    public class AdminstratorAccount : Entity
    {
        [Key]
        public Guid AdminId { get; set; }


        public string AdminUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AdminPassword { get; set; }
        public string AdminEmail { get; set; }

        public DateTime AdminAccountCreationDate { get; set; }


        public DateTime AdminLastLoginDate { get; set; }
    }
}
