using Subscription_based_marketing.Enums;

namespace Subscription_based_marketing.DTO
{
    public class ServiceDto
    {
        public Guid ServiceID { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public Duration ServiceDuration { get; set; }
        public string ServiceFeature { get; set; }
        public string ServiceTermsCondition { get; set; }
        public decimal ServicePrice { get; set; }
        public DateTime ServiceCreationDate { get; set; }
        public ServiceLevel ServiceLevel { get; set; }
        public bool ServiceIsPublish { get; set; }
        public virtual SellerDto seller { get; set; }

        public Guid SellerID { get; set; }
    }
}
