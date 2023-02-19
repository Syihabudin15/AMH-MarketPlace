using AMH_MarketPlace.DTOs.AuthDto;

namespace AMH_MarketPlace.Validations
{
    public static class ValidateRequest
    {
        public static bool ValidateNull(string req)
        {
            if(req == null) return false;
            return true;
        }

        public static bool ValidateRegister(RegisterRequest req) 
        {
            if (req.FirstName == null ||
                req.LastName == null ||
                req.PhoneNumber == null ||
                req.Email == null ||
                req.Password == null ||
                req.Password.Length < 6) return false;
            return true;
        }
    }
}
