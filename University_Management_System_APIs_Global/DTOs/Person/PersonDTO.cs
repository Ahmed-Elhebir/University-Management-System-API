
using University_Management_System_APIs_Global.Enums;

namespace University_Management_System_APIs_Global.DTOs.Person
{
    public class PersonDTO
    {
        // Primary Key
        public int PersonID { get; set; }
        
        // Properties
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalNumber { get; set; }
        public int NationalCountryID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }


        // Parameterized Constructor
        public PersonDTO(int personID, string firstName, string secondName, string? thirdName, string lastName, GenderEnum gender,
                    string phoneNumber, string? email, DateTime dateOfBirth, string nationalNumber, int nationalCountryID,
                    DateTime createdDate, DateTime lastModifiedDate, bool isDeleted)
        {
            this.PersonID = personID;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName ?? "";
            this.LastName = lastName;
            this.Gender = gender;
            this.PhoneNumber = phoneNumber;
            this.Email = email ?? "";
            this.DateOfBirth = dateOfBirth;
            this.NationalNumber = nationalNumber;
            this.NationalCountryID = nationalCountryID;
            this.CreatedDate = createdDate;
            this.LastModifiedDate = lastModifiedDate;
            this.IsDeleted = isDeleted;

        }
    }
}
