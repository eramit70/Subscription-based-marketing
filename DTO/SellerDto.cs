using System.ComponentModel.DataAnnotations;

namespace Subscription_based_marketing.DTO
{
    public class SellerDto
    {
        public Guid SellerID { get; set; }


        public string SellerUserName { get; set; }
        public string SellerEmail { get; set; }
        public string SellerName { get; set; }
        public string SellerDescription { get; set; }
        public long SellerPhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string SellerPassword { get; set; }
        public DateTime SellerAccountCreationDate { get; set; }
        public DateTime SellerLastLoginDate { get; set; }
    }
}
