using Subscription_based_marketing.Models.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Enums;
using URF.Core.EF.Trackable;

namespace Subscription_based_marketing.Models.Subscription
{
    public class SubscriptionDetails : Entity
    {
        [Key]
        public Guid SubscriptionID { get; set; }
        public virtual UserAccount user { get; set; }
        [Required]
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public virtual ServiceDetail Service { get; set; }

        [Required]
        [ForeignKey("ServiceID")]
        public Guid serviceID { get; set; }

        public DateTime SubscriptionStartDate { get; set; }

        public BillingFrequency SubscriptionBillingFrequency { get; set; }

        public DateTime SubscriptionNextBillingDate { get; set; }

        [Required]
        public Status SubscriptionStatus { get; set; }


    }
}
