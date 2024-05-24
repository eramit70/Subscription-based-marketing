using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Subscription_based_marketing.DataContext;
using Subscription_based_marketing.DTO;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Services;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.Services;

namespace Subscription_based_marketing.Services
{
    public class SellerAccountService : Service<SellerAccount>, ISellerAccountService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public SellerAccountService(ITrackableRepository<SellerAccount> repository,
            IMapper mapper,
            IUnitOfWork unitOfWork
           ) : base(repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task AddSellerAccountAsync(SellerDto sellerDto)
        {

            Guid ID = Guid.NewGuid();
            sellerDto.SellerID = ID;
            sellerDto.SellerAccountCreationDate = DateTime.Now;
            sellerDto.SellerLastLoginDate = DateTime.Now;
            sellerDto.SellerDescription = "";


            var sellerAccount = _mapper.Map<SellerAccount>(sellerDto);
            Repository.Insert(sellerAccount);
            await SaveAsync();
        }

        public async Task<SellerDto> GetSellerAccountByIDAsync(Guid ID)
        {
            var sellerAccount = await Repository.Queryable().
                Where(item => item.SellerID == ID).FirstOrDefaultAsync();

            var sellerDto = _mapper.Map<SellerDto>(sellerAccount);

            return sellerDto;
        }

        public async Task<bool> SellerLoginAsync(SellerDto sellerDto)
        {
            bool check = await Repository.Queryable().Where(item =>
            (item.SellerUserName == sellerDto.SellerUserName ||
            item.SellerEmail == sellerDto.SellerEmail) &&
            item.SellerPassword == sellerDto.SellerPassword)
            .AnyAsync();
            return check;
        }

        public void SellerLogoutAsync()
        {

        }

        public async Task SellerUpdateAsync(SellerDto sellerDto)
        {
            var sellerAccount = _mapper.Map<SellerAccount>(sellerDto);
            Repository.Update(sellerAccount);
            await SaveAsync();
        }

        public async Task<List<SellerDto>> GetSellerAccountListAsync()
        {
            var sellers = await Repository.Queryable().ToListAsync();

            return _mapper.Map<List<SellerDto>>(sellers);
        }
        public async Task<Guid> GetSellerIDByUserNameAsync(string selleruserName)
        {
            var seller = await Repository.Queryable().
                Where(item => item.SellerUserName == selleruserName).FirstOrDefaultAsync();

            return seller.SellerID;
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CheckDuplicateSellerAsync(string sellerUserName)
        {
            var duplicateSeller = await Repository.Queryable().
                Where(item => item.SellerUserName == sellerUserName).FirstOrDefaultAsync();

            if (duplicateSeller != null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateLastLoginDateByUserIdAsync(Guid sellerId)
        {
            var sellerAccount = await Repository.Queryable().
                Where(item => item.SellerID == sellerId).FirstOrDefaultAsync();

            sellerAccount.SellerLastLoginDate = DateTime.Now;
            Repository.Update(sellerAccount);
            await SaveAsync();
        }

        public async Task DeleteSellerAccountAsync(SellerDto sellerDto)
        {
            var sellerAccount = _mapper.Map<SellerAccount>(sellerDto);
            Repository.Delete(sellerAccount);
            await SaveAsync();
        }

        public async Task<bool> CheckDuplicateEmailAsync(string email)
        {
            var sellerAccount = await Repository.Queryable().Where(item => item.SellerEmail == email).FirstOrDefaultAsync();

            if (sellerAccount != null)
            {
                return true;
            }
            else return false;
        }
    }
}
