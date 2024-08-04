using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Accounts")]
    [PrimaryKey("UserId")]
    public class AccountModel
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Enums.Enums.AccountType AccountType { get; set; }
        public DateTime? DateActivate { get; set; }
    }
}
