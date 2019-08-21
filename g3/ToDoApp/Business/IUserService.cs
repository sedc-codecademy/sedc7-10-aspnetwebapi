using System;
using System.Collections.Generic;
using Models;

namespace Business
{
    public interface IUserService
    {
        void Register(UserModel model);
        IEnumerable<UserModel> GetAll();
    }
}
