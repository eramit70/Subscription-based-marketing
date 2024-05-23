using Subscription_based_marketing.Models.Seller;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Subscription_based_marketing.Enums;
using URF.Core.EF.Trackable;

namespace Subscription_based_marketing.Models.Services
{
    public class ServiceDetail : Entity
    {
        [Key]
        public Guid ServiceID { get; set; }
        public virtual SellerAccount seller { get; set; }

     
        [ForeignKey("SellerID")]
        public Guid SellerID { get; set; }


        public string ServiceTitle { get; set; }


        public string ServiceDescription { get; set; }

        public Duration ServiceDuration { get; set; }

        public string ServiceFeature { get; set; }

        public string ServiceTermsCondition { get; set; }
        public decimal ServicePrice { get; set; }

        public DateTime ServiceCreationDate { get; set; }

        public ServiceLevel ServiceLevel { get; set; }
        public bool ServiceIsPublish { get; set; }

    }
}
