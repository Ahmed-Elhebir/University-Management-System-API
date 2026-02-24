using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global
{
    public class CourseDataResult
    {
        public int CourseID { get; set; }
        public int ReturnValue { get; set; }

        public CourseDataResult(int courseID, int returnValue)
        {
            this.CourseID = courseID;
            this.ReturnValue = returnValue;
        }
    }
}
