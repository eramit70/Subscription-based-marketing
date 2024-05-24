using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Subscription_based_marketing.DataContext;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.User;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.Services;

namespace Subscription_based_marketing.Services
{
    public class UserAccountService : Service<UserAccount>, IUserAccountService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;
        // private readonly IRepository<UserAccount> __repository;
        //  private readonly HttpContextAccessor _httpContextAccessor;

        public UserAccountService(
            ITrackableRepository<UserAccount> repository,
            IUnitOfWork unitOfWork,

            IMapper mapper) : base(repository)
        {
            _unitofWork = unitOfWork;
            _mapper = mapper;

            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task AddUserAccountAsync(UserDto userDto)
        {

            Guid ID = Guid.NewGuid();
            userDto.UserID = ID;
            userDto.AccountCreationDate = DateTime.Now;
            userDto.UserLastLoginDate = DateTime.Now;
            userDto.SubscriptionStatus = false;
            userDto.State = "";
            userDto.City = "";

            var userAccount = _mapper.Map<UserAccount>(userDto);
            Repository.Insert(userAccount);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _unitofWork.SaveChangesAsync();

        }

        public async Task UserUpdateAsync(UserDto userDto)
        {
            var userAccount = _mapper.Map<UserAccount>(userDto);

            _mapper.Map(userDto, userAccount);

            Repository.Update(userAccount);
            await SaveAsync();

        }



        public async Task<List<UserDto>> GetUserAccountListAsync()
        {
            var users = await Repository.Queryable().ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
        public async Task DeleteUserAccountAsync(UserDto user)
        {
            var userEntity = _mapper.Map<UserAccount>(user);
            Repository.Delete(userEntity);
            await SaveAsync();
        }
        public async Task<bool> UserLoginAsync(UserDto userDto)
        {
            var check = await Repository.Queryable().
                Where(item => (item.UserName == userDto.UserName ||
            item.UserEmailAddress == userDto.UserEmailAddress)
           && item.UserPassword == userDto.UserPassword).AnyAsync();

            return check;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid Id)
        {
            var userAccount = await Repository.Queryable().
                Where(item => item.UserID == Id).FirstOrDefaultAsync();
            return _mapper.Map<UserDto>(userAccount);
        }



        public async Task<Guid> GetIDByUserNameAsync(string userName)
        {
            var user = await Repository.Queryable().
                Where(item => item.UserName == userName).FirstOrDefaultAsync();
            return user.UserID;
        }

        public async Task<bool> FIndSubscriptionStatusAsync(string userName)
        {
            var user = await Repository.Queryable().
                Where(item => item.UserName == userName).FirstOrDefaultAsync();

            return user.SubscriptionStatus;
        }

        public async Task<bool> CheckDuplicateUserAsync(string userName)
        {
            var duplicateUser = await Repository.Queryable().
                Where(item => item.UserName == userName).FirstOrDefaultAsync();

            if (duplicateUser != null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateLastLoginDateByUserIdAsync(Guid userId)
        {
            var userAccount = await Repository.Queryable().
                Where(item => item.UserID == userId).FirstOrDefaultAsync();

            userAccount.UserLastLoginDate = DateTime.Now;
            Repository.Update(userAccount);
            await SaveAsync();
        }


        public async Task SetTrueUserSubscriptionAsync(Guid ID)
        {
            var userAccount = await Repository.Queryable().
                Where(item => item.UserID == ID).FirstOrDefaultAsync();
            if (userAccount != null)
            {
                userAccount.SubscriptionStatus = true;
                Repository.Update(userAccount);
                await SaveAsync();
            }
        }

        public async Task<bool> CheckDuplicateEmailAsync(string email)
        {
            var userAccount = await Repository.Queryable().Where(item => item.UserEmailAddress == email).FirstOrDefaultAsync();
            if (userAccount != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
