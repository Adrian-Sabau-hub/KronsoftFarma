using KF.CommonModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KF.Services.Login
{
    public interface ILoginService
    {
        bool ValidateUser(string username, string password);
    }
}
