﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluNoro.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AllowAnonymousAttribute : Attribute
    {
        public AllowAnonymousAttribute()
        {
            
        }
    }
}
