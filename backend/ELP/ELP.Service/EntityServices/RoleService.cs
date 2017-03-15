using System;
using System.Collections.Generic;
using System.Text;
using ELP.Model.Entities;
using ELP.Service.Common;
using ELP.Model;
using System.Linq;

namespace ELP.Service
{
    public class RoleService : EntityService<Role>, IRoleService
    {
        public RoleService(IContext context) : base(context)
        {

        }

        public Role GetRoleById(int roleId)
        {
            return _dbset.SingleOrDefault(r => r.Id == roleId);
        }
    }
}
