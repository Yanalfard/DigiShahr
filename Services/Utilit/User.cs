using DataLayer.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Services.Services;

namespace Services.Utilit
{
    public class User : IUser
    {
        Core _core = new Core();
        public TblUser UserByPhoneNumber(string PhoneNumber)
        {
            return _core.User.Get().Where(u => u.TellNo == PhoneNumber).SingleOrDefault();
        }
    }
}
