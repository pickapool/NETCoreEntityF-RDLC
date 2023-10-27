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
        public int SanctionId { get; set; }
        public int StudentId { get; set; }
        public DateTime DateRecorded { get; set; }
        [ForeignKey("StudentId")]
        public StudentModel Student { get; set; } = new();
        public SanctionModel Sanction { get; set; } = new();
    }
}
