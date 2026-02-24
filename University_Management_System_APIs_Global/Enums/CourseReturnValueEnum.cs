using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global.Enums
{
    public enum CourseReturnValueEnum
    {
        SUCCESS = 1,
        UNEXPECTED_EXCEPTION = -1,
        COURSE_EXISTS = -2,
        COURSE_CODE_EXISTS = -3,
        COURSE_HAS_ENROLLMENT = -4
    }
}
