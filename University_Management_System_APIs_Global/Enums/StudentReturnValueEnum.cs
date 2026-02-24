using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global.Enums
{
    public enum StudentReturnValueEnum
    {
        SUCCESS = 1,
        UNEXPECTED_EXCEPTION = -1,
        EMAIL_EXISTS = -2
    }
}
