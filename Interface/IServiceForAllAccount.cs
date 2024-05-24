namespace Subscription_based_marketing.Interface
{
    public interface IServiceForAllAccount
    {
        Task<bool> CheckDuplicateUserNameInAllAccountByUserNameAsync(string userName);

        Task<bool> CheckDuplicateEmailAllAccountByEmailAsync(string email);
    }
}
