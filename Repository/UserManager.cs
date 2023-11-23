﻿using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using ViewModels.PaginationViewModels;
using ViewModels.ReservationViewModels;
using ViewModels.UserViewModels;

namespace Repository
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<PaginationViewModel<UserViewModel>> GetAll(int PageSize, int PageIndex)
        {
            PaginationDTO<UserViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
            };
            var usersInRole = await GetUsersInRoleAsync("user");
            return usersInRole.ToList().AsQueryable()
                .Select(u => u.ToUserViewModel())
                .ToPaginationViewModel(PaginationDTO);
        }
    }
}
