using Microsoft.EntityFrameworkCore;
using Subscription_based_marketing.Enums;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using URF.Core.EF.Trackable;

namespace Subscription_based_marketing.Models.ServiceControl
{
    public class ServiceAccessControl : Entity
    {
        [Key]
        public Guid ServiceAccessControlID { get; set; }

        public virtual UserAccount user { get; set; }
        [Required]
        [ForeignKey("UserID")]
        public Guid UserId { get; set; }

        public virtual SellerAccount seller { get; set; }
        [Required]
        [ForeignKey("SellerID")]
        public Guid SellerID { get; set; }
        public virtual ServiceDetail Service { get; set; }

        [Required]
        [ForeignKey("ServiceID")]
        public Guid serviceID { get; set; }


        public DateTime AccessStartTime { get; set; }
        public DateTime AccessEndTime { get; set; }
        public Status AccessStatus { get; set; }


      
    }
}
