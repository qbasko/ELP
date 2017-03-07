using System;
using System.Collections.Generic;
using System.Text;
using ELP.Model.Common;

namespace ELP.Model.Entities
{
    public class Role: Entity<int>
    {
        public string Name { get; set; }
    }
}
