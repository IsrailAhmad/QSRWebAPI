using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Models;
using EverGreenWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EverGreenWebApi.Controllers
{
    public class LoginController : ApiController
    {
        // GET: Login
        //public ActionResult Index()
        //{
        //    return View();
        //}

        static readonly ILoginRepository _repository = new LoginRepository();

        [HttpPost]
        public LoginModel UserLogin(LoginModel model)
        {
            LoginModel data = new LoginModel();
            using (qsr_androidEntities context = new qsr_androidEntities())
            {
                var check = context.loginmasters.Where(a => a.MobileNumber == model.MobileNumber && a.StoreId == model.StoreId).FirstOrDefault();
                if (check != null)
                {
                    data.CustomerId = check.CustomerId;
                    data.StoreId = (int)check.StoreId;
                    data.MobileNumber = check.MobileNumber;
                    data.Flag = "Registered";
                    data.CreatedOn = (DateTime)check.CreatedOn;
                }
                else
                {
                    loginmaster user = new loginmaster();
                    user.StoreId = model.StoreId;
                    user.MobileNumber = model.MobileNumber;
                    context.loginmasters.Add(user);
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        var resultData = context.loginmasters.Where(a => a.MobileNumber == model.MobileNumber && a.StoreId == model.StoreId).FirstOrDefault();
                        data.CustomerId = resultData.CustomerId;
                        data.StoreId = (int)resultData.StoreId;
                        data.MobileNumber = resultData.MobileNumber;
                        data.Flag = "Not Registered";
                        data.CreatedOn = (DateTime)resultData.CreatedOn;
                    }
                }
            }
            return data;
        }
    }
}