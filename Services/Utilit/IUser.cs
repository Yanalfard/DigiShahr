using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;

namespace Services.Utilit
{
    public interface IUser
    {
        TblUser UserByPhoneNumber(string PhoneNumber);
    }
}
