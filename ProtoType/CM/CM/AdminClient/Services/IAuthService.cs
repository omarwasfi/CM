using CM.Shared.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.AdminClient.Services
{
    public interface IAuthService
    {
        Task Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);
        Task Logout();
        Task<CurrentUser> CurrentUserInfo();
    }
}
