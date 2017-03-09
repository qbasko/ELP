using ELP.Model.Entities;
using ELP.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using ELP.Model;

namespace ELP.Service
{
    public class UserRoleService : EntityService<UserRole>, IUserRoleService
    {
        public UserRoleService(IContext context) : base(context)
        {
        }
    }
}
