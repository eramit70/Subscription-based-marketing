using Subscription_based_marketing.Enums;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Models.User;

namespace Subscription_based_marketing.DTO
{
    public class SubscriptionDto
    {
        public Guid SubscriptionID { get; set; }
        public virtual UserAccount user { get; set; }

        public Guid UserID { get; set; }
        public virtual ServiceDetail Service { get; set; }

        public Guid serviceID { get; set; }

        public DateTime SubscriptionStartDate { get; set; }

        public BillingFrequency SubscriptionBillingFrequency { get; set; }

        public DateTime SubscriptionNextBillingDate { get; set; }

        public Status SubscriptionStatus { get; set; }

    }
}
