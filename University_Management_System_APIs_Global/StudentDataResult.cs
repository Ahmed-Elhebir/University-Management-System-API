
namespace University_Management_System_APIs_Global
{
    public class StudentDataResult
    {
        public int ReturnValue { get; set; }
        public int? NewPersonID { get; set; }
        public int? NewStudentID { get; set; }
        public string? NewStudentNo { get; set; }

        public StudentDataResult(int returnValue, int? personID = -1, int? studentID = -1, string? studentNo = null)
        {
            this.NewPersonID = personID;
            this.NewStudentID = studentID;
            this.NewStudentNo = studentNo;
            this.ReturnValue = returnValue;
        }
    }
}
