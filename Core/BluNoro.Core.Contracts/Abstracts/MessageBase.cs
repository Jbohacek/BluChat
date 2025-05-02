using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Contracts.Interfaces;

namespace BluNoro.Core.Contracts.Abstracts
{
    public class MessageBase : ITable
    {
        public Guid Id { get; set; }



    }
}
