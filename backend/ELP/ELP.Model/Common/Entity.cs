﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Model.Common
{
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
