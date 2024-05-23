using Subscription_based_marketing.Enums;
using System.ComponentModel;

namespace Subscription_based_marketing.DTO
{
    public class ServiceDto
    {
        public Guid ServiceID { get; set; }
        [DisplayName("Service Title")]
        public string ServiceTitle { get; set; }

        [DisplayName("Service Description")]
        public string ServiceDescription { get; set; }

        [DisplayName("ServiceDuration")]
        public Duration ServiceDuration { get; set; }

        [DisplayName("Service Feature")]
        public string ServiceFeature { get; set; }

        [DisplayName("Terms & Condition")]
        public string ServiceTermsCondition { get; set; }

        [DisplayName("Service Price")]
        public decimal ServicePrice { get; set; }

        [DisplayName("Service Creation Date")]
        public DateTime ServiceCreationDate { get; set; }

        [DisplayName("Service Level")]
        public ServiceLevel ServiceLevel { get; set; }
        [DisplayName("Service is Public")]

        public bool ServiceIsPublish { get; set; }
        public virtual SellerDto seller { get; set; }

        public Guid SellerID { get; set; }
    }
}
