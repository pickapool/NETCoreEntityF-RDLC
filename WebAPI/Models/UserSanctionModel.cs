using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("UserSanctions")]
    [PrimaryKey("UserSanctionId")]
    public class UserSanctionModel
    {
        public UserSanctionModel() { }

        public int UserSanctionId { get; set; }
        public int SanctionId { get; set; }
        public int StudentId { get; set; }
        public DateTime DateRecorded { get; set; }
        [ForeignKey("StudentId")]
        public StudentModel Student { get; set; } = new();
        public SanctionModel Sanction { get; set; } = new();
        public decimal Amount { get ; set; }
        public bool IsPaid { get; set; }
        public byte[] SanctionImage { get; set; } = new byte[] { };
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public AccountModel Account { get; set; } = new();
        public int? MarkAsPaidById { get; set; }
        [ForeignKey("MarkAsPaidById")]
        public AccountModel? MarkAsPaidByAccount { get; set; } = new();
    }
}
