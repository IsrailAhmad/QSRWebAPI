using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public interface ILoginRepository : IDisposable
    {
        LoginModel UserLogin(LoginModel model);
    }
}