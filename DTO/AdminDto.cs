using System.ComponentModel.DataAnnotations;

namespace Subscription_based_marketing.DTO
{
    public class AdminDto
    {
        public Guid AdminId { get; set; }
        public string AdminUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AdminEmail { get; set; }
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }
        public DateTime AdminAccountCreationDate { get; set; }
        public DateTime AdminLastLoginDate { get; set; }
    }
}
