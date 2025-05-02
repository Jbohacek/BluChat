using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluNoro.Core.ClientFolder.EvenHandlers
{
    public class UserFailedVerificationHandler(string reason) : EventArgs
    {
        public string Reason { get; set; } = reason;

    }
}
