namespace pedagangpulsa.api.DTO.Request
{
    public class RegisterDTO
    {
        public string Owner { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Telegram { get; set; }
        public string? IpAddress { get; set; }
        public string? Pin { get; set; } = "11223344";
        public string? Password { get; set; } = "rahasia";
    }
}
