using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Subscription_based_marketing.DataContext;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.Models.Adminstrator;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.User;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Services;
using URF.Core.Abstractions.Trackable;
using URF.Core.Services;

namespace Subscription_based_marketing.Services
{
    public class AdminService : Service<AdminstratorAccount>, IAdminService
    {

        private readonly IMapper _mapper;
        private readonly ISellerAccountService _sellerAccountService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUnitOfWork _unitOfWork;
        public AdminService(ITrackableRepository<AdminstratorAccount> repository, IMapper mapper,

            ISellerAccountService sellerAccountService,
            IUnitOfWork unitOfWork,
            IUserAccountService userAccountService

            ) : base(repository)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;

            _sellerAccountService = sellerAccountService;
            _userAccountService = userAccountService;
        }



        public async Task AddAdminAccountAsync(AdminDto admin)
        {
            AdminstratorAccount adminEntity = _mapper.Map<AdminstratorAccount>(admin);
            Repository.Insert(adminEntity);
            await SaveAsync();
        }

        public async Task<bool> AdminLoginAsync(AdminDto admin)
        {
            bool check = await Repository.Queryable().Where(item =>
            (item.AdminEmail == admin.AdminEmail ||
            item.AdminUserName == admin.AdminUserName) &&
            item.AdminPassword == admin.AdminPassword).AnyAsync();

            return check;
        }

        public async Task AdminUpdateAsync(AdminDto admin)
        {
            var adminEntity = _mapper.Map<AdminstratorAccount>(admin);

            Repository.Update(adminEntity);
            await SaveAsync();
        }



        public async Task<AdminDto> GetAdminstratorByIdAsync(Guid ID)
        {
            var adminEntity = await Repository.Queryable().
                Where(item => item.AdminId == ID).FirstOrDefaultAsync();
            return _mapper.Map<AdminDto>(adminEntity);
        }



        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }

        public void AdminLogoutAsync()
        { }


        public async Task<bool> CheckDuplicateAdminAsync(string AdminuserName)
        {
            var duplicateUser = await Repository.Queryable().
                Where(item => item.AdminUserName == AdminuserName).FirstOrDefaultAsync();

            if (duplicateUser != null)
            {
                return true;
            }
            return false; ;
        }

        public async Task<Guid> GetAdminIDByUserNameAsync(string adminUserName)
        {
            var admin = await Repository.Queryable().
                Where(item => item.AdminUserName == adminUserName).FirstOrDefaultAsync();

            return admin.AdminId;
        }

        public async Task UpdateLastLoginDateByUserIdAsync(Guid adminId)
        {
            var adminAccount = await Repository.Queryable().
                Where(item => item.AdminId == adminId).FirstOrDefaultAsync();

            adminAccount.AdminLastLoginDate = DateTime.Now;

            Repository.Update(adminAccount);
            await SaveAsync();
        }
    }
}
