using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Sections")]
    [PrimaryKey("SectionId")]
    public class SectionModel
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
    }
}
