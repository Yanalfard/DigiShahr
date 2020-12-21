using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Services.Services;

namespace DigiShahr.Utilit
{
    public static class UserCrew
    {
        public static async Task<TblUser> UserByTellNo(string TellNo)
        {
            Core _core = new Core();
            return await Task.FromResult(_core.User.Get().Where(u => u.TellNo == TellNo).SingleOrDefault());
        }

        public static async Task<bool> UserIsExist(LoginViewModel loginViewModel)
        {
            Core _core = new Core();
            return await Task.FromResult(
                _core.User.Get()
                .Any(u => u.TellNo == loginViewModel.TellNo &&
                u.Password == Cryptography.SHA256(loginViewModel.Password) &&
                u.IsActive == true));
        }
    }
}
