using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Sanctions")]
    [PrimaryKey("SanctionId")]
    public class SanctionModel
    {
        public int SanctionId { get; set; }
        public string SanctionName { get; set; } = string.Empty;
    }
}
