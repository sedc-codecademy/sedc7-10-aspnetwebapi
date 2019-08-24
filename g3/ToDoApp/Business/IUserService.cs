using System;
using System.Collections.Generic;
using Models;

namespace Business
{
    public interface IUserService
    {
        void Register(RegisterModel model);
        IEnumerable<UserModel> GetAll();
        UserModel Authenticate(LoginModel model);
    }
}
