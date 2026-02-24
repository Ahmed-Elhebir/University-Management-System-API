using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global.Enums
{
    public enum EnrollmentReturnValueEnum
    {
        SUCCESS = 1,
        UNEXPECTED_EXCEPTION = -1,
        STUDENT_NOT_EXIST = -2,
        STUDENT_ALREADY_ENROLLED = -3,
        COURSE_NOT_EXIST = -4,
        COURSE_IS_FULL = -5,
        ENROLLMENT_NOT_EXIST = -6,
        FINAL_GRADE_ALREADY_ASSIGNED = -7,
        NO_GRADED_ENROLLMENTS_FOUND = -8,
        CANNOT_DELETE_ENROLLMENT_WITH_FINAL_GRADE = -9
    }
}
