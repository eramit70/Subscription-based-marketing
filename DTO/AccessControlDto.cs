using Subscription_based_marketing.Enums;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Models.User;

namespace Subscription_based_marketing.DTO
{
    public class AccessControlDto
    {
        public Guid ServiceAccessControlID { get; set; }
        public virtual UserAccount user { get; set; }    
        public Guid UserId { get; set; }
        public virtual SellerAccount seller { get; set; }
        public Guid SellerID { get; set; }
        public virtual ServiceDetail Service { get; set; }
        public Guid serviceID { get; set; }
        public DateTime AccessStartTime { get; set; }
        public DateTime AccessEndTime { get; set; }
        public Status AccessStatus { get; set; }

    }
}
