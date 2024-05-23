using Subscription_based_marketing.Enums;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Models.User;
using System.ComponentModel;

namespace Subscription_based_marketing.DTO
{
    public class SubscriptionDto
    {
        public Guid SubscriptionID { get; set; }
        public virtual UserAccount user { get; set; }

        public Guid UserID { get; set; }
        public virtual ServiceDetail Service { get; set; }

        public Guid serviceID { get; set; }

        [DisplayName("Subscription Starting Date")]
        public DateTime SubscriptionStartDate { get; set; }

        [DisplayName("Billing Frequency")]
        public BillingFrequency SubscriptionBillingFrequency { get; set; }

        [DisplayName("Next Billing Date")]
        public DateTime SubscriptionNextBillingDate { get; set; }

        [DisplayName("Subscription Status")]
        public Status SubscriptionStatus { get; set; }

    }
}
