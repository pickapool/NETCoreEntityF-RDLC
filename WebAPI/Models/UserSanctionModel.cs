using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("UserSanctions")]
    [PrimaryKey("UserSanctionId")]
    public class UserSanctionModel
    {
        public int UserSanctionId { get; set; }
        public int UserId { get; set; }
        public string SanctionName { get; set; } = string.Empty;
    }
}
