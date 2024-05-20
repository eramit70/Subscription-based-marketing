using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Enums;
using Subscription_based_marketing.Models.Services;

namespace Subscription_based_marketing.Interface
{
    public interface IServiceListService
    {
        Task CreateServiceAsync(ServiceDto serviceDetail);
        Task DeleteServiceAsync(ServiceDto serviceDetail);
        Task<List<ServiceDto>> GetAllServiceListAsync();
        Task UpdateServiceAsync(ServiceDto serviceDetail);
        Task<ServiceDto> GetDetailsServiceAsync(Guid ID);
        Task SaveChangeAsync();
        Task<Duration> GetDurationByServiceIDAsync(Guid ID);
        Task<List<ServiceDto>> GetServiceListBySeller(ServiceDto serviceDetail);
        Task<ServiceDto> GetServiceDetailsBySubServiceIDAsync(Guid SubServiceId);
        Task<List<ServiceDto>> UseSevicesListAsync(Guid sellerID);




    }
}
