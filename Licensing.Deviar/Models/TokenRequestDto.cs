namespace UnitedCoachwaysCRM.Models
{
    public class TokenRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Persist { get; set; }
    }
}
