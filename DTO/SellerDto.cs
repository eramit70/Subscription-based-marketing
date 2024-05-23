using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Subscription_based_marketing.DTO
{
    public class SellerDto
    {
        public Guid SellerID { get; set; }
        [DisplayName("User Name")]
        public string SellerUserName { get; set; }
        [DisplayName("Email Address")]
        public string SellerEmail { get; set; }
        [DisplayName("Seller Name")]
        public string SellerName { get; set; }
        [DisplayName("About Seller")]
        public string SellerDescription { get; set; }
        [DisplayName("Mobile Number")]
        public long SellerPhoneNumber { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string SellerPassword { get; set; }
        [DisplayName("Account Creation Date")]
        public DateTime SellerAccountCreationDate { get; set; }
        [DisplayName("Last Login Date")]
        public DateTime SellerLastLoginDate { get; set; }
    }
}
