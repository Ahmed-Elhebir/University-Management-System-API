
namespace University_Management_System_APIs_Global.DTOs.Country
{
    public class CountryDTO
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }


        public CountryDTO(int countryID, string countryName)
        {
            this.CountryID = countryID;
            this.CountryName = countryName;
        }
    }
}
