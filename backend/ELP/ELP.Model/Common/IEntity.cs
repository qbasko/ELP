using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Model.Common
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
