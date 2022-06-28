using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Enums
{   
    public enum EmailVerificationStatus
    {
        SendVerificationNotification = 1,
        Verify = 2,
        Decline = 3,
    }
}
