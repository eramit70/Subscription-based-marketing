using Subscription_based_marketing.Models.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using URF.Core.EF.Trackable;

namespace Subscription_based_marketing.Models.Seller
{
    public class SellerAccount : Entity
    {
        [Key]

        public Guid SellerID { get; set; }

        [Required]
        [Display(Name = "Enter User Name")]
        public string SellerUserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Enter Email Address")]
        public string SellerEmail { get; set; }
        [Display(Name = "Enter Name")]
        public string SellerName { get; set; }

        public string SellerDescription { get; set; }

        [DataType(DataType.PhoneNumber)]
        public long SellerPhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Enter Password")]
        public string SellerPassword { get; set; }
        public DateTime SellerAccountCreationDate { get; set; }
        public DateTime SellerLastLoginDate { get; set; }

        /* public ServiceDetail service { get; set; }
         [Required]
         [ForeignKey("ServiceID")]
         public Guid ServiceID { get; set; }
 */
    }
}
