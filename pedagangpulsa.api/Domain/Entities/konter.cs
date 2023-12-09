using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pedagangpulsa.api.Domain.Entities
{
    [Table("tblKonter")]
    public class konter
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Owner { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }

        [Column("WA")]
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Telegram { get; set; }
        public string IpAddress { get; set; }
        public string Pin { get; set; }
        public string Password { get; set; }
        public int saldo { get; set; } = 0;
        public bool isActive { get; set; } = true;
        public bool isVerived { get; set; } = false;
    }
}
