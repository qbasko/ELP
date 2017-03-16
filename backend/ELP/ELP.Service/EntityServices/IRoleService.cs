using ELP.Model.Entities;
using ELP.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Service
{
    public interface IRoleService : IEntityService<Role>
    {
        Role GetRoleById(int roleId);
    }
}
