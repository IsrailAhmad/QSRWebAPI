using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EverGreenWebApi.Models;
using EverGreenWebApi.DBHelper;

namespace EverGreenWebApi.Repository
{
    public class LoginRepository: ILoginRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

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
                        data.CreatedOn = (DateTime)check.CreatedOn;
                    }
                }
            }
            return data;
        }        
    }
}