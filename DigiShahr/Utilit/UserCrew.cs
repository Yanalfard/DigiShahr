using DataLayer.Models;
using DataLayer.ViewModel;
using Services.Services;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using System;

namespace DigiShahr.Utilit
{
    public static class UserCrew
    {
        public static async Task<TblUser> UserByTellNo(string TellNo)
        {
            Core _core = new Core();
            return await Task.FromResult(_core.User.Get(u => u.TellNo == TellNo).SingleOrDefault());
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

        public static async Task<bool> UserDuplication(string TellNo)
        {
            Core _core = new Core();
            return await Task.FromResult(
                _core.User.Get()
                .Any(u => u.TellNo == TellNo.Replace(" ", ""))
                );
        }

        public static async Task<bool> UserCreator(CreateAccountViewModel createAccountViewModel)
        {
            Core _core = new Core();
            TblUser NewUser = new TblUser();
            NewUser.TellNo = createAccountViewModel.TellNo.Replace(" ", "");
            NewUser.Name = createAccountViewModel.Name;
            NewUser.Password = Cryptography.SHA256(createAccountViewModel.Password.Replace(" ", ""));
            NewUser.RoleId = 3;
            NewUser.Role = _core.Role.GetById(3);
            NewUser.DateCreated = DateTime.Now;
            NewUser.IsActive = false;
            NewUser.Lat = createAccountViewModel.Lat;
            NewUser.Lon = createAccountViewModel.Lon;
            NewUser.NaighborhoodId = createAccountViewModel.NaighborhoodId;
            NewUser.Address = createAccountViewModel.Address;
            var CodeCreator = Guid.NewGuid().ToString();
            string Code = CodeCreator.Substring(CodeCreator.Length - 5);
            NewUser.Auth = Code;
            await SendSms.Send(NewUser.TellNo, NewUser.Auth, "DigiShahrAuthAccount");
            _core.User.Add(NewUser);
            _core.User.Save();
            return await Task.FromResult(true);
        }
    }
}
